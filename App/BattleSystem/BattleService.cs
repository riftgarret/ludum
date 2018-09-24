using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.BattleSystem.Entity;

namespace App.BattleSystem
{
    public class BattleService : MonoBehaviour, IBattleController
    {

        public float unitOfTime = 1f;

        // test data
        private EnemyPartySO enemyParty;
        private PCPartySO pcParty;


        private BattlePresenter presenter;

        protected BattleEntityManager mEntityManager;

        

        // UNITY LIFE CYCLE

        void Awake()
        {
            presenter = new BattlePresenter();

            PartyComponent partyComponent = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<PartyComponent>();
            EnemyComponent enemyComponent = GameObject.FindGameObjectWithTag(Tags.ENEMY).GetComponent<EnemyComponent>();

            presenter.Initialize(partyComponent, enemyComponent);
        }
        

        void Update()
        {
            // Life cycle order            
            // event queue
            presenter.ProcessEventQueue();

            // actions required 
            presenter.ProcessActionsRequireQueue();

            
            presenter.OnTimeDelta(unitOfTime * Time.deltaTime);            
        }



        // INTERFACE CALLS TO DIGEST EVENTS

        public void PostBattleEvent(IBattleEvent e)
        {
            Debug.Log("On event: " + e);
            mBattleEventQueue.Enqueue(e);
        }

        public void PostActionRequired(BattleEntity entity)
        {
            Debug.Log("entity decision required: " + entity);
            if (!mActionRequiredQueue.Contains(entity))
            {
                mActionRequiredQueue.Enqueue(entity);
            }
        }

        public void ExecuteCombat(ICombatOperation combatOperation)
        {
            mCombatExecutor.Execute(combatOperation);
        }

        /// <summary>
        /// in game clock thats handled on update when the user interface isnt stalled for current active character	/// </summary>
        /// <value><c>true</c> if active game time; otherwise, <c>false</c>.</value>
        protected virtual bool isTimeActive
        {
            get
            {
                return mGameState == GameState.ACTIVE;
            }
        }



        // BattleService required methods
        protected override void OnBattleEvent(IBattleEvent e)
        {
            // TODO forward to combat log

            // evaluate if the game is over, or we have won
            switch (e.EventType)
            {
                case BattleEventType.DEATH:
                    CheckForVictoryOrAnnilate(!e.SrcEntity.isPC); // 
                    break;
            }
        }

        protected override void OnInitialize()
        {
            // initialize entities for other methods in start        
            mBattleTimeQueue.InitEntities(mEntityManager.allEntities);
        }    

        protected override void OnActionRequired(BattleEntity entity)
        {
            if (entity is PCBattleEntity)
            {
                mTurnManager.QueuePC((PCBattleEntity)entity);
            }
            else if (entity is EnemyBattleEntity)
            {
                EnemyBattleEntity npc = (EnemyBattleEntity)entity;
                IBattleAction enemyAction = npc.enemyCharacter.skillResolver.ResolveAction(mEntityManager, npc);
                mBattleTimeQueue.SetAction(entity, enemyAction);
            }
        }





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
            this.mGameState = isEnemies ? GameState.VICTORY : GameState.LOSS;

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

        // implemented IBattleController to expose components
        public BattleEntityManager entityManager
        {
            get { return mEntityManager; }
        }


        public TurnManager turnManager
        {
            get { return mTurnManager; }
        }

        
    }
} 

    
