using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.BattleSystem.Entity;

namespace App.BattleSystem
{
    public class BattleService : MonoBehaviour
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
    }
} 

    
