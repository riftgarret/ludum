using App.BattleSystem.Action;
using App.BattleSystem.Combat.Operation;
using App.BattleSystem.Combat.Operation.App.BattleSystem.Combat.Operation;
using App.BattleSystem.Entity;
using App.BattleSystem.Events;
using App.BattleSystem.Turn;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static App.BattleSystem.Action.BattleAction;

namespace App.BattleSystem
{
    /// <summary>
    /// Main logic that flows through this scene is handled by this presenter in a MVP relationship.
    /// Where this is the presenter, other components generated will represent the views.  
    /// </summary>
    public class BattlePresenter
    {

        private TurnManager turnManager = new TurnManager();

        private BattleTimeQueue battleTimeQueue = new BattleTimeQueue();

        private CombatOperationExecutor combatExecutor = new CombatOperationExecutor();

        private Queue<IBattleEvent> battleEventQueue = new Queue<IBattleEvent>();

        private Queue<BattleEntity> actionRequiredQueue = new Queue<BattleEntity>();

        private BattleEntityManager entityManager;

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
            

        }



        public void Initialize(PartyComponent partyComponent, EnemyComponent enemyComponent)
        {
            entityManager = new BattleEntityManager(partyComponent, enemyComponent);

            // on actions selected 
            turnManager.OnCompleteDelegate += OnActionSelected;

            // on action executed
            combatExecutor.OnCombatEventDelegate += PostBattleEvent;

            // TODO hook in entities from map
            gameState = GameState.INTRO;
           
        }

        private void OnActionSelected(BattleEntity entity, IBattleAction action)
        {
            battleTimeQueue.SetAction(entity, action);
        }

        /// <summary>
        /// Event 
        /// </summary>
        /// <param name="delta"></param>
        public void OnTimeDelta(float delta)
        {
            // tic game 
            if (isTimeActive)
            {
                battleTimeQueue.IncrementTimeDelta(delta);
            }
        }

        private void PostBattleEvent(IBattleEvent e)
        {
            Debug.Log("On event: " + e);
            battleEventQueue.Enqueue(e);
        }

        private void PostActionRequired(BattleEntity entity)
        {
            Debug.Log("entity decision required: " + entity);
            if (!actionRequiredQueue.Contains(entity))
            {
                actionRequiredQueue.Enqueue(entity);
            }
        }

        private void ExecuteCombat(ICombatOperation combatOperation)
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
            }
        }

        public void ProcessActionsRequireQueue()
        {
            while (actionRequiredQueue.Count > 0)
            {
                BattleEntity entity = actionRequiredQueue.Dequeue();
                OnActionRequired(entity);
            }
        }

        /// <summary>
        /// Action is required for this character.
        /// </summary>
        /// <param name="entity"></param>
        private void OnActionRequired(BattleEntity entity)
        {
            if (entity is PCBattleEntity)
            {
                turnManager.QueuePC((PCBattleEntity)entity);
            }
            else if (entity is EnemyBattleEntity)
            {
                EnemyBattleEntity npc = (EnemyBattleEntity)entity;
                IBattleAction enemyAction = npc.enemyCharacter.skillResolver.ResolveAction(entityManager, npc);
                battleTimeQueue.SetAction(entity, enemyAction);
            }
        }


        private bool isTimeActive
        {
            get
            {
                return turnManager.currentEntity == null && gameState == GameState.ACTIVE;
            }
        }

        /// <summary>
        /// Check case for if battle should end.
        /// </summary>
        /// <param name="isEnemies"></param>
        private void CheckForVictoryOrAnnilate(bool isEnemies)
        {
            BattleEntity[] entities = isEnemies ? (BattleEntity[])entityManager.enemyEntities : (BattleEntity[])entityManager.pcEntities;


            foreach (BattleEntity entity in entities)
            {
                if (entity.Character.curHP > 0)
                {
                    return; // we found an alive player, no way to achieve either state
                }
            }

            // if we got here, it means everyone is dead
            this.gameState = isEnemies ? GameState.VICTORY : GameState.LOSS;

            // not sure if this is the best place to put this, perhaps in its own script
            if (isEnemies)
            {
                Debug.Log("Victory");
            }
            else
            {
                Debug.Log("Defeat");
            }
        }
    }
 
}
