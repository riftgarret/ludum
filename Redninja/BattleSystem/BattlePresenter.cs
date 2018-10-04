using System;
using System.Collections.Generic;
using Redninja.BattleSystem.Entities;
using Redninja.BattleSystem.Turn;

namespace Redninja.BattleSystem
{
	/// <summary>
	/// Main logic that flows through this scene is handled by this presenter in a MVP relationship.
	/// Where this is the presenter, other components generated will represent the views.  
	/// </summary>
	public class BattlePresenter : IBattlePresenter
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
		// Maybe consider using a class from a library for performance?
		private readonly SortedList<float, IBattleOperation> battleOpQueue = new SortedList<float, IBattleOperation>();

        //private AISkillResolver aiSkillResolver = new AISkillResolver();

		// I feel this should be part of the view
		private readonly PCDecisionManager pcDecisionManager = new PCDecisionManager();

		public event Action<IBattleEvent> BattleEventOccurred;

		// Make this public
		protected GameState gameState;

        // a way to we can only be in a certain state when the game is active
        protected enum GameState
        {
            INTRO,
            ACTIVE,
            VICTORY,
            LOSS
        }

		// Make this public or merge into below
		/// <summary>
		/// Check if time should be active. This can be false due to the game state being
		/// over or that the user needs to select a skill.
		/// </summary>
		private bool IsTimeActive => pcDecisionManager.currentEntity == null && gameState == GameState.ACTIVE;

		/// <summary>
		/// Update Time.
		/// </summary>
		/// <param name="timeDelta"></param>
		public void IncrementGameClock(float timeDelta)
		{
			if (IsTimeActive)
			{
				clock.IncrementTime(timeDelta);
			}
		}

		public BattlePresenter(IBattleView view, ICombatExecutor combatExecutor)
		{
			this.view = view;
			this.combatExecutor = combatExecutor;

            // notifies entity needs decision
            entityManager.DecisionRequired += OnActionRequired;

            // on a player's successful action selected 
            //pcDecisionManager.OnActionSelectedDelegate = OnActionSelected;

            // on combat events 
            //combatExecutor.OnCombatEventDelegate = OnBattleEvent;
        }

        /// <summary>
        /// Initialize presenter to load up views and prepare for lifecycle calls.
        /// </summary>
        public void Initialize(IEnumerable<IBattleEntity> units)
        {

			//entityManager.LoadEntities(partyComponent, enemyComponent);

			gameState = GameState.INTRO;
		}

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
			if (entity.IsPlayerControlled)
			{
				// Prompt player for decision
				//pcDecisionManager.QueuePC(entity);
			}
			else
			{
				// Move AI skill decision to BattleEntity

				//EnemyBattleEntity npc = (EnemyBattleEntity)entity;
				//IBattleAction enemyAction = aiSkillResolver.ResolveAction(entityManager, npc);
				//entityManager.SetAction(entity, enemyAction);
			}
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


		// Deal with Unity GUI later
		#region View updates
		/// <summary>
		/// Handle our Unity GUI loops here.
		/// </summary>
		public void OnGUI()
		{
			UpdateEntityGUI();
		}

		/// <summary>
		/// Update GUI for characters
		/// </summary>
		private void UpdateEntityGUI()
        {
            foreach (BattleEntity entity in entityManager.AllEntities)
            {
                //view.SetEntityHps(entity, entity.CurrentHP, entity.MaxHP);
                //view.SetEntityActionPercent(entity, BattleHelper.GetActionPercent(entity));
            }
        }
		#endregion
	}    
}
