using UnityEngine;
using System.Collections;

public class BattleActionInitiative : IBattleAction {

	private float mRecoverTime;

	public BattleActionInitiative(float initiativeTime) {
		mRecoverTime = initiativeTime;
	}

	public void OnExecuteAction (float actionClock)
	{
		throw new System.NotImplementedException ();
	}

	public float timePrepare {
		get {
			return 0;
		}
	}

	public float timeAction {
		get {
			return 0;
		}
	}

	public float timeRecover {
		get {
			return mRecoverTime;
		}
	}
}
