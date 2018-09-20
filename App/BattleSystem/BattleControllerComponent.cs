using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleControllerComponent : BattleService, IBattleController {

    // test data
    private EnemyPartySO enemyParty;
    private PCPartySO pcParty;

    
    private BattleTimeQueue mBattleTimeQueue;            
    private PCTurnManagerComponent mTurnManager;

    

    // BattleService required methods
    protected override void OnBattleEvent(IBattleEvent e) {        
        // TODO forward to combat log

        // evaluate if the game is over, or we have won
        switch (e.EventType) {
            case BattleEventType.DEATH:
                CheckForVictoryOrAnnilate(!e.SrcEntity.isPC); // 
                break;
        }
    }    

    protected override void OnInitialize() {
        // override parameters        
        mBattleTimeQueue = new BattleTimeQueue();
        mTurnManager = GetComponent<PCTurnManagerComponent>();
		mTurnManager.OnCompleteDelegate += OnActionSelected;

        // initialize entities for other methods in start        
        mBattleTimeQueue.InitEntities(mEntityManager.allEntities);
    }    

    protected override void OnTimeDelta(float delta) {
        mBattleTimeQueue.IncrementTimeDelta(delta);
    }

    protected override void OnActionRequired(BattleEntity entity) {
        if (entity is PCBattleEntity) {
            mTurnManager.QueuePC((PCBattleEntity)entity);
        }
        else if(entity is EnemyBattleEntity) {
            EnemyBattleEntity npc = (EnemyBattleEntity)entity;
            IBattleAction enemyAction = npc.enemyCharacter.skillResolver.ResolveAction(mEntityManager, npc);
			mBattleTimeQueue.SetAction(entity, enemyAction);
        }
    }

    protected void OnActionSelected(BattleEntity entity, IBattleAction action) {
        mBattleTimeQueue.SetAction(entity, action);
    }
    


    private void CheckForVictoryOrAnnilate(bool isEnemies) {
        BattleEntity[] entities = isEnemies ? (BattleEntity[])entityManager.enemyEntities : (BattleEntity[])entityManager.pcEntities;


        foreach (BattleEntity entity in entities) {
            if (entity.character.curHP > 0) {
                return; // we found an alive player, no way to achieve either state
            }
        }

        // if we got here, it means everyone is dead
        this.mGameState = isEnemies ? GameState.VICTORY : GameState.LOSS;

        // not sure if this is the best place to put this, perhaps in its own script
        if (isEnemies) {
            Debug.Log("Victory");
        }
        else {
            Debug.Log("Defeat");
        }
    }

    // implemented IBattleController to expose components
    public BattleEntityManagerComponent entityManager {
        get { return mEntityManager; }
    }


    public PCTurnManagerComponent turnManager {
        get { return mTurnManager; }
    }

	protected override bool isTimeActive {
		get {
			return mTurnManager.currentEntity == null && base.isTimeActive;
		}
	}
}
