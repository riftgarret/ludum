using System;
using System.Collections.Generic;
using Davfalcon.Revelator;
using Davfalcon.Randomization;
using Redninja.Actions;
using Redninja.Decisions;
using Redninja.Entities;
using Redninja.Skills;
using Redninja.Targeting;
using Ninject;

namespace Redninja
{
	/// <summary>
	/// Main logic that flows through this scene is handled by this presenter in a MVP relationship.
	/// Where this is the presenter, other components generated will represent the views.  
	/// </summary>
	public class BattlePresenter : IBattlePresenter
	{
		public class Clock : IClock
		{
			public float Time { get; private set; }
			public event Action<float> Tick;
			public void IncrementTime(float timeDelta)
			{
				Time += timeDelta;
				Tick?.Invoke(timeDelta);
			}
		}

		private Clock clock;
		private readonly IKernel kernel;
		private readonly IBattleView view;
		private readonly ICombatExecutor combatExecutor;
		private readonly IBattleEntityManager entityManager;
		private readonly PlayerDecisionManager playerDecisionManager;
		private readonly Queue<IBattleEntity> decisionQueue = new Queue<IBattleEntity>();		
		// Maybe consider using a class from a library for performance?
		private readonly SortedList<float, IBattleOperation> battleOpQueue = new SortedList<float, IBattleOperation>();

		public event Action<IBattleEvent> BattleEventOccurred;

		/// <summary>
		/// Gets the presenter's state.
		/// </summary>
		public GameState State { get; private set; } = GameState.Intro;

		/// <summary>
		/// Check if time should be active. This can be false due to the game state being
		/// over or that the user needs to select a skill.
		/// </summary>
		public bool IsTimeActive => State == GameState.Active;

		public static IBattlePresenter CreatePresenter(IBattleView view, ICombatExecutor combatExecutor)
		{
			IKernel kernel = new StandardKernel();
			kernel.Bind<IBattleView>().ToConstant(view);
			kernel.Bind<IBattleEntityManager>().To<BattleEntityManager>().InSingletonScope();
			kernel.Bind<ICombatExecutor>().ToConstant(combatExecutor);
			kernel.Bind<PlayerDecisionManager>().ToSelf().InSingletonScope();
			kernel.Bind<IBattlePresenter>().To<BattlePresenter>().InSingletonScope();
			kernel.Bind<IClock>().To<Clock>().InSingletonScope();
			kernel.Bind<Clock>().ToSelf().InSingletonScope();
			return kernel.Get<IBattlePresenter>();
		}

		public BattlePresenter(ICombatExecutor combatExecutor,
			IBattleEntityManager entityManager,
			PlayerDecisionManager playerDecisionManager,
			IBattleView view,
			IKernel kernel,
			Clock clock)
		{
			this.kernel = kernel;
			this.combatExecutor = combatExecutor;
			this.entityManager = entityManager;
			this.playerDecisionManager = playerDecisionManager;
			this.view = view;
			this.clock = clock;

			entityManager.DecisionRequired += OnActionRequired;			
			combatExecutor.BattleEventOccurred += OnBattleEventOccurred;

			// This mess is to keep view control in the presenter
			playerDecisionManager.WaitingForDecision += WaitForDecision;
			playerDecisionManager.WaitResolved += Start;
			playerDecisionManager.TargetingSkill += view.SetViewModeTargeting;
			playerDecisionManager.TargetingEnded += view.SetViewModeDefault;
			view.ActionSelected += playerDecisionManager.OnActionSelected;
			view.SkillSelected += playerDecisionManager.OnSkillSelected;
			view.TargetSelected += playerDecisionManager.OnTargetSelected;
			view.TargetingCanceled += playerDecisionManager.OnTargetingCanceled;

			view.SetBattleModel(entityManager);
		}

		#region Setup and control
		/// <summary>
		/// Initialize presenter to load up views and prepare for lifecycle calls.
		/// </summary>
		public void Initialize()
		{
			entityManager.InitializeBattlePhase();

			// this value is temp until we assign an initiative per character

			foreach (IBattleEntity entity in entityManager.AllEntities)
			{
				OnActionSelected(entity, new WaitAction(new RandomInteger(1, 10).Get()));
			}
		}

		public void Start()
		{
			State = GameState.Active;
		}

		public void Dispose()
		{
			kernel.Dispose();
		}

		private void AddBattleEntity(IBattleEntity entity)
		{
			entity.ActionDecider.ActionSelected += OnActionSelected;
			entityManager.AddBattleEntity(entity, clock);
		}

		public void AddCharacter(IUnit character, int row, int col)
			=> AddCharacter(character, playerDecisionManager, 0, row, col);

		public void AddCharacter(IUnit character, IActionDecider actionDecider, int team, int row, int col)
		{
			IBattleEntity entity = new BattleEntity(character, actionDecider, combatExecutor)
			{
				Team = team
			};
			entity.MovePosition(row, col);
			AddBattleEntity(entity);
		}

		public void AddCharacter(IBuilder<IUnit> builder, int row, int col)
			=> AddCharacter(builder.Build(), row, col);

		public void AddCharacter(IBuilder<IUnit> builder, IActionDecider actionDecider, int team, int row, int col)
			=> AddCharacter(builder.Build(), actionDecider, team, row, col);

		/// <summary>
		/// Update game clock.
		/// </summary>
		/// <param name="timeDelta"></param>
		public void IncrementGameClock(float timeDelta)
		{
			if (IsTimeActive)
			{
				clock.IncrementTime(timeDelta);
			}
		}
		#endregion

		#region Decision processing
		/// <summary>
		/// Handles a new action selected for an <see cref="IBattleEntity"/>.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="action"></param>
		private void OnActionSelected(IBattleEntity entity, IBattleAction action)
		{
			action.BattleOperationReady += OnBattleOperationReady;
			entityManager.SetAction(entity, action);
		}

		/// <summary>
		/// Enqueues an <see cref="IBattleEntity"/> that is waiting for a decision.
		/// </summary>
		/// <param name="entity"></param>
		private void OnActionRequired(IBattleEntity entity)
		{
			// Is this dupe check necessary?
			if (!decisionQueue.Contains(entity))
			{
				decisionQueue.Enqueue(entity);
			}
		}

		/// <summary>
		/// Requests a decision for the <see cref="IBattleEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		private void ProcessDecision(IBattleEntity entity)
		{
			entity.ActionDecider.ProcessNextAction(entity, entityManager);
		}

		/// <summary>
		/// Process actions required incase multiple characters need to make a decision.
		/// </summary>
		public void ProcessDecisionQueue()
		{
			while (decisionQueue.Count > 0)
			{
				IBattleEntity entity = decisionQueue.Dequeue();
				ProcessDecision(entity);
			}
		}
		#endregion

		#region Battle operations
		/// <summary>
		/// Enqueues an <see cref="IBattleOperation"/> for processing.
		/// </summary>
		/// <param name="startTime"></param>
		/// <param name="operation"></param>
		private void OnBattleOperationReady(float startTime, IBattleOperation operation)
		{
			battleOpQueue.Add(startTime, operation);
		}

		/// <summary>
		/// Processes the event queue.
		/// </summary>
		public void ProcessBattleOperationQueue()
		{
			while (battleOpQueue.Count > 0)
			{
				IBattleOperation op = battleOpQueue.Values[0];
				battleOpQueue.RemoveAt(0);

				op.Execute(entityManager, combatExecutor);
			}
		}

		private void OnBattleEventOccurred(IBattleEvent battleEvent)
		{
			BattleEventOccurred?.Invoke(battleEvent);
			view.BattleEventOccurred(battleEvent);
		}
		#endregion

		#region Update loop
		/// <summary>
		/// Handle our Unity GUI loops here.
		/// </summary>
		public void Update()
		{
			ProcessBattleOperationQueue();
			//UpdateView();
			ProcessDecisionQueue();
		}

		/// <summary>
		/// Update the view.
		/// </summary>
		private void UpdateView()
		{
			//foreach (IBattleEntity entity in entityManager.AllEntities)
			//{
			//	view.UpdateEntity(entity);
			//}
		}


		#endregion

		#region View control
		private void WaitForDecision(IBattleEntity entity)
		{
			State = GameState.Paused;
			view.NotifyDecisionNeeded(entity);
		}
		#endregion
	}
}
