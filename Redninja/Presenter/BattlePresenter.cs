using System;
using System.Collections.Generic;
using Davfalcon.Builders;
using Davfalcon.Randomization;
using Davfalcon.Revelator;
using Ninject;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Decisions;
using Redninja.Components.Decisions.Player;
using Redninja.Components.Operations;
using Redninja.Entities;
using Redninja.Events;
using Redninja.View;

namespace Redninja.Presenter
{
	/// <summary>
	/// Main logic that flows through this scene is handled by this presenter in a MVP relationship.
	/// Where this is the presenter, other components generated will represent the views.  
	/// </summary>
	public class BattlePresenter : IBattlePresenter
	{
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
			kernel.Bind<IBattleEntityManager, IBattleModel>().To<BattleEntityManager>().InSingletonScope();
			kernel.Bind<ICombatExecutor>().ToConstant(combatExecutor);
			kernel.Bind<PlayerDecisionManager>().ToSelf().InSingletonScope();
			kernel.Bind<IDecisionHelper>().To<DecisionHelper>().InSingletonScope();
			kernel.Bind<IBattlePresenter>().To<BattlePresenter>().InSingletonScope();
			kernel.Bind<IClock>().To<Clock>().InSingletonScope();
			kernel.Bind<Clock>().ToSelf().InSingletonScope();
			return kernel.Get<IBattlePresenter>();
		}

		public BattlePresenter(IKernel kernel)
		{
			this.kernel = kernel;

			combatExecutor = kernel.Get<ICombatExecutor>();
			entityManager = kernel.Get<IBattleEntityManager>();
			playerDecisionManager = kernel.Get<PlayerDecisionManager>();
			view = kernel.Get<IBattleView>();
			clock = kernel.Get<Clock>();

			entityManager.DecisionRequired += OnActionRequired;
			combatExecutor.BattleEventOccurred += OnBattleEventOccurred;
			combatExecutor.BattleEventOccurred += view.OnBattleEventOccurred;
			playerDecisionManager.WaitingForDecision += WaitForDecision;
			playerDecisionManager.WaitResolved += Start;

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

			foreach (IBattleEntity entity in entityManager.Entities)
			{
				OnActionSelected(entity, new WaitAction(new RandomInteger(1, 10).Get()));
			}
		}

		public void Start()
		{
			State = GameState.Active;
		}

		public void Pause()
		{
			State = GameState.Paused;
		}

		public void Dispose()
		{
			kernel.Dispose();
			foreach (IBattleEntity entity in entityManager.Entities)
			{
				entity.Dispose();
			}
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

		public void AddCharacter(Func<Unit.Builder, IBuilder<IUnit>> builderFunc, int row, int col)
			=> AddCharacter(Unit.Build(builderFunc), row, col);

		public void AddCharacter(Func<Unit.Builder, IBuilder<IUnit>> builderFunc, IActionDecider actionDecider, int team, int row, int col)
			=> AddCharacter(Unit.Build(builderFunc), actionDecider, team, row, col);

		/// <summary>
		/// Update game clock. This drives the presenter.
		/// </summary>
		public void IncrementGameClock(float timeDelta)
		{
			if (IsTimeActive)
			{
				// This will cause actions to trigger
				clock.IncrementTime(timeDelta);
			}

			// The queue contains operations that already triggered, so we always want to process this
			ProcessBattleOperationQueue();

			// This function contains a time check
			ProcessDecisionQueue();
		}
		#endregion

		#region Decision processing
		/// <summary>
		/// Enqueues an <see cref="IBattleEntity"/> that is waiting for a decision.
		/// </summary>
		private void OnActionRequired(IBattleEntity entity)
		{
			// Is this dupe check necessary?
			if (!decisionQueue.Contains(entity))
			{
				decisionQueue.Enqueue(entity);
			}
		}

		/// <summary>
		/// Handles a new action selected for an <see cref="IBattleEntity"/>.
		/// </summary>
		private void OnActionSelected(IUnitModel entity, IBattleAction action)
		{
			action.BattleOperationReady += OnBattleOperationReady;
			// This cast should be safe as long as we control the implementations carefully
			entityManager.SetAction(entity as IBattleEntity, action);
		}

		/// <summary>
		/// Requests the next action for the <see cref="IBattleEntity"/>.
		/// </summary>
		private void ProcessDecision(IBattleEntity entity)
		{
			entity.ActionDecider.ProcessNextAction(entity, entityManager);
		}

		/// <summary>
		/// Process entities waiting for a decision.
		/// </summary>
		public void ProcessDecisionQueue()
		{
			// A waiting player character should pause the game so we don't want to keep processing decisions
			while (IsTimeActive && decisionQueue.Count > 0)
			{
				IBattleEntity entity = decisionQueue.Dequeue();
				ProcessDecision(entity);
			}
		}

		/// <summary>
		/// Pause the game execution to wait for a player decision.
		/// </summary>
		/// <param name="entity"></param>
		private void WaitForDecision(IUnitModel entity)
		{
			Pause();
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

		// This is not needed right now, but may be useful to implement other functionality that responds to battle events
		private void OnBattleEventOccurred(IBattleEvent battleEvent)
		{
			BattleEventOccurred?.Invoke(battleEvent);
		}
		#endregion
	}
}
