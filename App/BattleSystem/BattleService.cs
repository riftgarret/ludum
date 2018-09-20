using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class BattleService : MonoBehaviour, IBattleService {

    public float unitOfTime = 1f;

    protected GameState mGameState;
    private Queue<IBattleEvent> mBattleEventQueue;
    private Queue<BattleEntity> mActionRequiredQueue;
    private CombatOperationExecutor mCombatExecutor;
    protected BattleEntityManagerComponent mEntityManager;

    // a way to we can only be in a certain state when the game is active
    protected enum GameState {
        INTRO,
        ACTIVE,
        VICTORY,
        LOSS
    }

    // UNITY LIFE CYCLE

    void Awake() {        
        mBattleEventQueue = new Queue<IBattleEvent>();
        mActionRequiredQueue = new Queue<BattleEntity>();
        mGameState = GameState.INTRO;
        mCombatExecutor = new CombatOperationExecutor();
        mEntityManager = GetComponent<BattleEntityManagerComponent>();

        // first create our battle system for other components to initialize
        BattleSystem.OnServiceStart(this);
    }

    void OnDestroy() {
        BattleSystem.OnServiceDestroy();
    }

    void Start() {
        OnInitialize(); 
		mGameState = GameState.ACTIVE;
    }

    void Update() {
        // Life cycle order            
        // event queue
		ProcessEventQueue ();

		// actions required 
		ProcessActionsRequireQueue ();

        // tic game 
        if (isTimeActive) {
            OnTimeDelta(unitOfTime * Time.deltaTime);
        }
    }


	/// <summary>
	/// Processes the event queue.
	/// </summary>
	private void ProcessEventQueue() {
		while (mBattleEventQueue.Count > 0) {
			IBattleEvent battleEvent = mBattleEventQueue.Dequeue();
			Debug.Log("ProcessEventQueue: " + battleEvent);
		}
	}

	private void ProcessActionsRequireQueue() {
		while (mActionRequiredQueue.Count > 0) {
			BattleEntity entity = mActionRequiredQueue.Dequeue();
			OnActionRequired(entity);
		}
	}

    // INTERFACE CALLS TO DIGEST EVENTS
    
    public void PostBattleEvent(IBattleEvent e) {
        Debug.Log("On event: " + e);
        mBattleEventQueue.Enqueue(e);
    }

    public void PostActionRequired(BattleEntity entity) {
        Debug.Log("entity decision required: " + entity);
        if (!mActionRequiredQueue.Contains(entity)) {
            mActionRequiredQueue.Enqueue(entity);
        }
    }

    public void ExecuteCombat(ICombatOperation combatOperation) {
        mCombatExecutor.Execute(combatOperation);
    }

    protected abstract void OnInitialize();

    protected abstract void OnTimeDelta(float delta);

    protected abstract void OnActionRequired(BattleEntity entity);    

    protected abstract void OnBattleEvent(IBattleEvent e);    
	
    /// <summary>
    /// in game clock thats handled on update when the user interface isnt stalled for current active character	/// </summary>
    /// <value><c>true</c> if active game time; otherwise, <c>false</c>.</value>
    protected virtual bool isTimeActive {
        get {
			return mGameState == GameState.ACTIVE;
        }
    }

}
    
