using System;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Ninject;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Combat.Events;
using Redninja.Components.Decisions;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Decisions.Player;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Data;
using Redninja.Entities;
using Redninja.System;
using Redninja.View;

namespace Redninja.Presenter
{
	/// <summary>
	/// Main logic that flows through this scene is handled by this presenter in a MVP relationship.
	/// Where this is the presenter, other components generated will represent the views.  
	/// </summary>
	public class BattlePresenter : IBattlePresenter, IPresenterConfiguration,
		IBaseCallbacks, ISkillsCallbacks, IMovementCallbacks, ITargetingCallbacks
	{
		private readonly IKernel kernel;
		private readonly DataManager dataManager;
		private readonly Clock clock;
		private readonly IBattleView view;
		private readonly ICombatExecutor combatExecutor;
		private readonly IBattleEntityManager entityManager;
		private readonly PlayerDecisionManager playerDecisionManager;
		private readonly ProcessingQueue<IBattleEntity> decisionQueue;
		private readonly IBattleEventProcessor entityEventTriggerProcessor;
		private readonly PriorityProcessingQueue<float, IBattleOperation> battleOpQueue;
		private readonly SystemProvider systemProvider;

		public event Action<ICombatEvent> BattleEventOccurred;

		/// <summary>
		/// Gets the presenter's state.
		/// </summary>
		public GameState State { get; private set; } = GameState.Intro;

		/// <summary>
		/// Check if time should be active. This can be false due to the game state being
		/// over or that the user needs to select a skill.
		/// </summary>
		public bool TimeActive => State == GameState.Active;

		public static IBattlePresenter CreatePresenter(IBattleView view, Func<CombatResolver.Builder, CombatResolver.Builder> combatRules)
			=> CreatePresenter(view, new CombatExecutor(combatRules));

		private static IBattlePresenter CreatePresenter(IBattleView view, ICombatExecutor combatExecutor)
		{
			IKernel kernel = new StandardKernel();
			kernel.Bind<IBattleView>().ToConstant(view);
			kernel.Bind<ICombatExecutor>().ToConstant(combatExecutor);
			kernel.Bind<IDataManager, DataManager>().To<DataManager>().InSingletonScope();			
			kernel.Bind<ISystemProvider>().To<SystemProvider>().InSingletonScope();
			kernel.Bind<IClock, Clock>().To<Clock>().InSingletonScope();
			kernel.Bind<IBattleEntityManager, IBattleModel>().To<BattleEntityManager>().InSingletonScope();
			kernel.Bind<PlayerDecisionManager>().ToSelf().InSingletonScope();
			kernel.Bind<IDecisionHelper>().To<DecisionHelper>().InSingletonScope();
			kernel.Bind<IBattleEventProcessor>().To<EntityBattleEventProcessor>().InSingletonScope();
			kernel.Bind<IBattlePresenter>().To<BattlePresenter>().InSingletonScope();
			return kernel.Get<IBattlePresenter>();
		}

		public BattlePresenter(IKernel kernel)
		{
			this.kernel = kernel;

			dataManager = kernel.Get<DataManager>();
			combatExecutor = kernel.Get<ICombatExecutor>();
			entityManager = kernel.Get<IBattleEntityManager>();
			playerDecisionManager = kernel.Get<PlayerDecisionManager>();
			entityEventTriggerProcessor = kernel.Get<IBattleEventProcessor>();
			systemProvider = (SystemProvider) kernel.Get<ISystemProvider>();
			view = kernel.Get<IBattleView>();
			clock = kernel.Get<Clock>();

			decisionQueue = new ProcessingQueue<IBattleEntity>(entity =>
				entity.ActionDecider.ProcessNextAction(entity, entityManager));

			battleOpQueue = new PriorityProcessingQueue<float, IBattleOperation>(op =>
				op.Execute(entityManager, combatExecutor));

			BindEvents();

			view.SetBattleModel(entityManager);
		}

		private void BindEvents()
		{
			entityManager.ActionNeeded += decisionQueue.Enqueue;
			entityManager.ActionSet += (e, action) => action.BattleOperationReady += battleOpQueue.Enqueue;
			combatExecutor.BattleEventOccurred += e => BattleEventOccurred?.Invoke(e);
			combatExecutor.BattleEventOccurred += view.OnBattleEventOccurred;
			combatExecutor.BattleEventOccurred += entityEventTriggerProcessor.ProcessEvent;
			playerDecisionManager.WaitingForDecision += e => Pause();
			playerDecisionManager.WaitingForDecision += view.OnDecisionNeeded;
			playerDecisionManager.WaitResolved += Start;
			playerDecisionManager.WaitResolved += view.Resume;
		}

		#region Configuration
		public void Configure(Action<IPresenterConfiguration> configFunc)
			=> configFunc(this);

		public void AddPC(IUnit character, int teamId, Coordinate position, ISkillProvider skillProvider)
		{			
			IBattleEntity entity = AddCharacter(character, teamId, position, playerDecisionManager);
			systemProvider.SetSkillProvider(entity, skillProvider);
		}

		public void AddNPC(IUnit character, int teamId, Coordinate position, AIBehavior aiBehavior, string nameOverride = null)
		{
			IBattleEntity entity = AddCharacter(character, teamId, position, new AIActionDecider(aiBehavior, kernel.Get<IDecisionHelper>()), nameOverride);
			systemProvider.SetSkillProvider(entity, new AISkillProvider(aiBehavior));
		}

		private IBattleEntity AddCharacter(IUnit character, int team, Coordinate position, IActionDecider actionDecider, string nameOverride = null)
		{
			BattleEntity entity = new BattleEntity(character, actionDecider, combatExecutor)
			{
				Team = team				
			};
			if(nameOverride != null) entity.SetNameOverride(nameOverride);
			entity.MovePosition(position.Row, position.Column);
			entityManager.AddEntity(entity);
			return entity;
		}

		public void SetTeamGrid(int team, Coordinate gridSize)
			=> entityManager.AddGrid(team, gridSize);
		#endregion

		#region Control
		/// <summary>
		/// Initialize presenter to load up views and prepare for lifecycle calls.
		/// </summary>
		public void Initialize()
		{
			entityManager.InitializeBattlePhase();
			view.SetViewMode(this);
		}

		public void Start()
		{
			State = GameState.Active;
		}

		public void Pause()
		{
			State = GameState.Paused;
		}

		/// <summary>
		/// Update game clock. This drives the presenter.
		/// </summary>
		public void IncrementGameClock(float timeDelta)
		{
			if (TimeActive)
			{
				// This will cause actions to trigger
				clock.IncrementTime(timeDelta);
			}

			// The queue contains operations that already triggered, so we always want to process this
			battleOpQueue.Process();

			// Process any pending decisions until we need to wait for one
			decisionQueue.ProcessWhile(() => TimeActive);
		}

		public void Dispose()
		{
			entityManager.Dispose();
			kernel.Dispose();
		}
		#endregion

		#region View callbacks
		void IBaseCallbacks.SelectUnit(IUnitModel entity)
		{
			playerDecisionManager.SetEntityContext(entity);
			view.SetViewMode(playerDecisionManager.ActionsContext, this);
		}

		#region Action selection
		void ISkillsCallbacks.InitiateMovement(IUnitModel entity)
		{
			playerDecisionManager.SetMovementContext(entity);
			view.SetViewMode(playerDecisionManager.MovementContext, this);
		}

		void ISkillsCallbacks.SelectSkill(IUnitModel entity, ISkill skill)
		{
			playerDecisionManager.SetTargetingContext(entity, skill);
			view.SetViewMode(playerDecisionManager.TargetingContext, this);
		}

		void ISkillsCallbacks.Wait(IUnitModel entity)
		{
			playerDecisionManager.Resolve(entity, new WaitAction(5));
			view.SetViewMode(this);
		}

		void ISkillsCallbacks.Cancel()
		{
			playerDecisionManager.ExitContext();
			view.SetViewMode(this);
		}
		#endregion

		#region Movement
		void IMovementCallbacks.UpdatePath(Coordinate point)
		{
			playerDecisionManager.MovementContext.AddPoint(point);
			// Should we set view after intermediate steps?
		}

		void IMovementCallbacks.Confirm()
		{
			playerDecisionManager.Resolve();
			view.SetViewMode(this);
		}

		void IMovementCallbacks.Cancel()
		{
			playerDecisionManager.ExitContext();
			view.SetViewMode(playerDecisionManager.ActionsContext, this);
		}
		#endregion

		#region Targeting
		void ITargetingCallbacks.SelectTarget(ISelectedTarget target)
		{
			playerDecisionManager.TargetingContext.SelectTarget(target);

			if (playerDecisionManager.TargetingContext.Ready)
			{
				playerDecisionManager.Resolve();
				view.SetViewMode(this);
			}
		}

		void ITargetingCallbacks.Cancel()
		{
			if (!playerDecisionManager.TargetingContext.Back())
			{
				playerDecisionManager.ExitContext();
				view.SetViewMode(playerDecisionManager.ActionsContext, this);
			}
		}
		#endregion
		#endregion
	}
}
