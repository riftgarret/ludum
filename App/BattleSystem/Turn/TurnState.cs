using UnityEngine;
using System.Collections;

public class TurnState {

	// action phase
	public enum Phase {
		REQUIRES_INPUT,
		PREPARE,
		EXECUTE,
		RECOVER
	}
	
	
	// inner class to manage the turn state

	private BattleEntity entity;
	public IBattleAction action;
	public Phase phase;
	public float turnClock;
	public float turnComplete;
	
	public float turnPercent {
		get {
			if(turnComplete == 0) {
				return 0;
			}
			return Mathf.Min(turnClock / turnComplete, 1f);
		}
	}
	
	public TurnState(BattleEntity entity) {
		this.entity = entity;
	}
	
	/// <summary>
	/// Sets the action. This will reset all variables to Prepare
	/// </summary>
	/// <param name="action">Action.</param>
	public void SetAction(IBattleAction action) {
		this.action = action;
		SetPhase(Phase.PREPARE);
	}
	
	/// <summary>
	/// Sets the phase. Reset all state variables
	/// </summary>
	/// <param name="phase">Phase.</param>
	public void SetPhase(Phase newPhase) {
		turnClock = 0;
		this.phase = newPhase;
		switch(newPhase) {
		case Phase.REQUIRES_INPUT:
			action = null;
			turnComplete = -1;
			break;
		case Phase.PREPARE:
			turnComplete = action.timePrepare;
			break;
		case Phase.EXECUTE:
			turnComplete = action.timeAction;
			break;
		case Phase.RECOVER:
			turnComplete = action.timeRecover;
			break;
		}

		CheckNextPhase(false);
	}
	
	/// <summary>
	/// Checks the next phase. Sometimes we can start with 0 turn, lets skip that
	/// </summary>
	private void CheckNextPhase(bool force) {
		if(force || (phase != Phase.REQUIRES_INPUT && turnComplete == 0)) {
			switch(phase) {
			case Phase.PREPARE:
				SetPhase(Phase.EXECUTE);
				break;
			case Phase.EXECUTE:
				SetPhase(Phase.RECOVER);
				break;
			case Phase.RECOVER:
				SetPhase(Phase.REQUIRES_INPUT);
				break;
				
			}
		}
	}

	/// <summary>
	/// Increment Game states
	/// </summary>
	/// <param name="gameClockDelta">Game clock delta.</param>
	public void IncrementGameClock(float gameClockDelta) {
		if(phase != Phase.REQUIRES_INPUT) {
			turnClock += gameClockDelta;
			// important if we go over our clock complete we still
			// call on execute so if there were any actions that were calculated
			// to happen at the end of the turn, it will still execute
			if(phase == Phase.EXECUTE) {
				entity.OnExecuteTurn(this);
			}
			if(turnClock > turnComplete) {
				// increment turn
				CheckNextPhase(true);

				// if the new phase is requires input lets notify our entity
				if(phase == Phase.REQUIRES_INPUT) {
					entity.OnRequiresInput(this);
				}
			}
		}
	}
}
