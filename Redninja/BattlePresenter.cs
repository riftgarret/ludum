using System;
using System.Collections.Generic;
using Davfalcon.Randomization;
using Redninja.Actions;
using Redninja.Decisions;
using Redninja.Entities;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja
{
	/// <summary>
	/// Main logic that flows through this scene is handled by this presenter in a MVP relationship.
	/// Where this is the presenter, other components generated will represent the views.  
	/// </summary>
	public class BattlePresenter : IBattlePresenter, IBattleViewCallbacks
	{
		private class Clock : IClock
		{
			public float Time { get; private set; }
			public event Action<float> Tick;
			public void IncrementTime(float timeDelta)
			{
				Time += timeDelta;
				Tick?.Invoke(timeDelta);
			}
		}

		private Clock clock = new Clock();
		private readonly IBattleView view;
		private readonly ICombatExecutor combatExecutor;
		private readonly IBattleEntityManager entityManager = new BattleEntityManager();
		private readonly Queue<IBattleEntity> decisionQueue = new Queue<IBattleEntity>();
        private readonly DecisionManager decisionManager;
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

		public BattlePresenter(IBattleView view, ICombatExecutor combatExecutor)
		{
			this.view = view;
			this.combatExecutor = combatExecutor;

			entityManager.DecisionRequired += OnActionRequired;

            decisionManager = new DecisionManager(entityManager);
		}

		#region Setup and control
		/// <summary>
		/// Initialize presenter to load up views and prepare for lifecycle calls.
		/// </summary>
		public void Initialize()
		{
			entityManager.InitializeBattlePhase();

			// this value is temp until we assign an initiative per character
			//SetAction(new WaitAction(new RandomInteger(1, 5).Get()));

			foreach (IBattleEntity entity in entityManager.AllEntities)
			{
				OnActionSelected(entity, new WaitAction(new RandomInteger(1, 5).Get()));
			}
		}

		public void AddBattleEntities(IEnumerable<IBattleEntity> entities)
		{
			foreach (IBattleEntity entity in entities)
			{
				AddBattleEntity(entity);
			}
		}

		public void AddBattleEntity(IBattleEntity entity)
		{
			entity.ActionDecider.ActionSelected += OnActionSelected;
			entityManager.AddBattleEntity(entity, clock);
		}

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
			State = GameState.Active;
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
			if (entity.IsPlayerControlled)
			{
				State = GameState.Paused;
			}

			// Need to figure out the best form of game state to give the decider
			entity.ActionDecider.ProcessNextAction(entity, this);
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
		/// <param name="operation"></param>
		private void OnBattleOperationReady(IBattleOperation operation)
		{
			operation.BattleEventOccurred += OnBattleEventOccurred;
			battleOpQueue.Add(operation.ExecutionStartTime, operation);
		}

		/// <summary>
		/// Processes the event queue.
		/// </summary>
		public void ProcessBattleOperationQueue()
		{
			while (battleOpQueue.Count > 0)
			{
				IBattleOperation op = battleOpQueue[0];
				battleOpQueue.RemoveAt(0);

				op.Execute(entityManager, combatExecutor);
			}
		}

		private void OnBattleEventOccurred(IBattleEvent battleEvent)
		{
			BattleEventOccurred?.Invoke(battleEvent);
		}
		#endregion

		#region Update loop
		// This can probably happen together with clock increment
		// Game state should not change while presenter state is paused
		/// <summary>
		/// Handle our Unity GUI loops here.
		/// </summary>
		public void Update()
		{
			UpdateView();
		}

		/// <summary>
		/// Update the view.
		/// </summary>
		private void UpdateView()
		{
			foreach (IBattleEntity entity in entityManager.AllEntities)
			{
				view.UpdateEntity(entity);
			}
		}


        #endregion

        #region battleview callbacks
        void IBattleViewCallbacks.OnSkillSelected(IBattleEntity entity, ICombatSkill skill)
        {
            var targetingInfo = decisionManager.GetSelectableTargets(entity, skill);
            view.SetViewModeTargeting(targetingInfo);
        }

        void IBattleViewCallbacks.OnTargetSelected(IBattleEntity entity, ICombatSkill skill, SelectedTarget target)
        {
            var battleAction = decisionManager.CreateAction(entity, skill, target);
            entity.SetAction(battleAction);
            view.SetViewModeDefault();            
        }
        #endregion
    }
}
