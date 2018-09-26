using App.BattleSystem.Actions;
using App.BattleSystem.AI;
using App.BattleSystem.Combat.Operation;
using App.BattleSystem.Combat.Operation.App.BattleSystem.Combat.Operation;
using App.BattleSystem.Entity;
using App.BattleSystem.Events;
using App.BattleSystem.Turn;
using App.Core.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.BattleSystem
{
    /// <summary>
    /// Main logic that flows through this scene is handled by this presenter in a MVP relationship.
    /// Where this is the presenter, other components generated will represent the views.  
    /// </summary>
    public class BattlePresenter
    {

        private PCDecisionManager pcDecisionManager = new PCDecisionManager();       

        private CombatOperationExecutor combatExecutor = new CombatOperationExecutor();        

        private Queue<IBattleEvent> battleEventQueue = new Queue<IBattleEvent>();

        private Queue<BattleEntity> actionRequiredQueue = new Queue<BattleEntity>();

        private AISkillResolver aiSkillResolver = new AISkillResolver();

        private BattleEntityManager entityManager = new BattleEntityManager();

        private IBattleView view;

        protected GameState gameState;

        // a way to we can only be in a certain state when the game is active
        protected enum GameState
        {
            INTRO,
            ACTIVE,
            VICTORY,
            LOSS
        }        
        
        public BattlePresenter()
        {
            // notifies entity needs decision
            entityManager.OnDecisionRequiredDelegate = OnActionRequired;

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
        /// <param name="view"></param>
        /// <param name="partyComponent"></param>
        /// <param name="enemyComponent"></param>
        public void Initialize(
            IBattleView view,
            PartyComponent partyComponent, 
            EnemyComponent enemyComponent)
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

        /// <summary>
        /// Update Time.
        /// </summary>
        /// <param name="delta"></param>
        public void OnTimeDelta(float delta)
        {
            // tic game 
            if (IsTimeActive)
            {
                entityManager.IncrementTimeDelta(delta);
            }
        }

        /// <summary>
        /// Handle our Unity GUI loops here.
        /// </summary>
        public void OnGUI()
        {
            UpdateEntityGUI();
        }

        private void OnBattleEvent(IBattleEvent e)
        {
            Debug.Log("On event: " + e);
            battleEventQueue.Enqueue(e);
        }

        /// <summary>
        /// Delegate from BattleEntity. When a character needs to make a decision. Lets queue it up.
        /// </summary>
        /// <param name="entity"></param>
        private void OnActionRequired(BattleEntity entity)
        {
            Debug.Log("entity decision required: " + entity);
            if (!actionRequiredQueue.Contains(entity))
            {
                actionRequiredQueue.Enqueue(entity);
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
        public void ProcessActionsRequireQueue()
        {
            while (actionRequiredQueue.Count > 0)
            {
                BattleEntity entity = actionRequiredQueue.Dequeue();
                ProcessActionRequired(entity);
            }
        }

        /// <summary>
        /// Action is required for this character.
        /// </summary>
        /// <param name="entity"></param>
        private void ProcessActionRequired(BattleEntity entity)
        {
            if (entity is PCBattleEntity)
            {
                pcDecisionManager.QueuePC((PCBattleEntity)entity);
            }
            else if (entity is EnemyBattleEntity)
            {
                EnemyBattleEntity npc = (EnemyBattleEntity)entity;
                IBattleAction enemyAction = aiSkillResolver.ResolveAction(entityManager, npc);
                entityManager.SetAction(entity, enemyAction);
            }
        }

        /// <summary>
        /// Check if time should be active. This can be false due to the game state being
        /// over or that the user needs to select a skill.
        /// </summary>
        private bool IsTimeActive
        {
            get
            {
                return pcDecisionManager.currentEntity == null && gameState == GameState.ACTIVE;
            }
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
