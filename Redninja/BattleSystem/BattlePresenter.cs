using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.BattleSystem.Actions;
using Redninja.BattleSystem.AI;
using Redninja.BattleSystem.Combat.Operation;
using Redninja.BattleSystem.Combat.Operation.Redninja.BattleSystem.Combat.Operation;
using Redninja.BattleSystem.Entity;
using Redninja.BattleSystem.Events;
using Redninja.BattleSystem.Turn;
using Redninja.Core.Characters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.BattleSystem
{
    /// <summary>
    /// Main logic that flows through this scene is handled by this presenter in a MVP relationship.
    /// Where this is the presenter, other components generated will represent the views.  
    /// </summary>
    public class BattlePresenter : IGameClock
    {
		private ICombatResolver combatResolver;

        private PCDecisionManager pcDecisionManager = new PCDecisionManager();       

        private CombatOperationExecutor combatExecutor = new CombatOperationExecutor();        

        private Queue<IBattleEvent> battleEventQueue = new Queue<IBattleEvent>();

        private Queue<IBattleEntity> decisionQueue = new Queue<IBattleEntity>();

        //private AISkillResolver aiSkillResolver = new AISkillResolver();

        private BattleEntityManager entityManager = new BattleEntityManager();

        private IBattleView view;

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
		/// <param name="delta"></param>
		public void IncrementGameClock(float delta)
		{
			if (IsTimeActive)
				entityManager.IncrementGameClock(delta);
		}

		public BattlePresenter(ICombatResolver combatResolver)
        {
			this.combatResolver = combatResolver;

            // notifies entity needs decision
            entityManager.DecisionRequired += OnActionRequired;

			// I don't think we need this event, just manually inject the combat executor dependency into the BattleAction
            // notifies entity's action should start to be executed
            entityManager.OnExecuteOperationDelegate = OnExecuteCombat;

            // on a player's successful action selected 
            pcDecisionManager.OnActionSelectedDelegate = OnActionSelected;

            // on combat events 
            combatExecutor.OnCombatEventDelegate = OnBattleEvent;
        }

        /// <summary>
        /// Initialize presenter to load up views and prepare for lifecycle calls.
        /// </summary>
        public void Initialize(IBattleView view, IEnumerable<IBattleEntity> units)
        {
            this.view = view;

            entityManager.LoadEntities(partyComponent, enemyComponent);
            
            gameState = GameState.INTRO;           
        }

        /// <summary>
        /// When a PC or enemy AI has selected an action.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="action"></param>
        private void OnActionSelected(BattleEntity entity, IBattleAction action)
        {
            entityManager.SetAction(entity, action);
        }

        private void OnBattleEvent(IBattleEvent e)
        {
            battleEventQueue.Enqueue(e);
        }

        /// <summary>
        /// Delegate from BattleEntity. When a character needs to make a decision. Lets queue it up.
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
        /// Delegate from BattleEntity. When a character's Action executes an operation.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="battleAction"></param>
        private void OnExecuteCombat(BattleEntity entity, ICombatOperation combatOperation)
        {
            combatExecutor.Execute(combatOperation);
        }

        /// <summary>
        /// Processes the event queue.
        /// </summary>
        public void ProcessEventQueue()
        {
            while (battleEventQueue.Count > 0)
            {
                IBattleEvent battleEvent = battleEventQueue.Dequeue();
                Debug.Log("ProcessEventQueue: " + battleEvent);

                // temp before we find a good place to resolve.
                switch (battleEvent.EventType)
                {
                    case BattleEventType.DEATH:
                        if(BattleHelper.CheckForDefeat(entityManager))
                        {
                            // TODO should kick off another event to signal we are done.
                            this.gameState = GameState.LOSS;
                        }
                        else if(BattleHelper.CheckForVictory(entityManager))
                        {
                            this.gameState = GameState.VICTORY;
                        }                                                                        
                        break;
                }
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

        /// <summary>
        /// Action is required for this character.
        /// </summary>
        /// <param name="entity"></param>
        private void ProcessDecision(IBattleEntity entity)
        {
            if (entity.IsPlayerControlled)
            {
                pcDecisionManager.QueuePC(entity);
            }
            else
            {
				// Move AI skill decision to BattleEntity
                EnemyBattleEntity npc = (EnemyBattleEntity)entity;
                IBattleAction enemyAction = aiSkillResolver.ResolveAction(entityManager, npc);
                entityManager.SetAction(entity, enemyAction);
            }
        }



		// Deal with Unity GUI later

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
                view.SetEntityHps(entity, entity.CurrentHP, entity.MaxHP);
                view.SetEntityActionPercent(entity, BattleHelper.GetActionPercent(entity));
            }
        }
    }    
}
