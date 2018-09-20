using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class CombatSkillSO : SkillSO , ICombatSkill{
	

	public float timePrepare = 1f;	
	public float timeAction = 1f;	
	public float timeRecover = 1f;

	public TargetingRule targetRule;
	public CombatRound [] combatRounds = new CombatRound[1];

	// TODO other preconditions to check, ie, weapons equiped, skills active, etc

	public float TimePrepare {
		get {
			return timePrepare;
		}
	}

	public float TimeAction {
		get {
			return timeAction;
		}
	}

	public float TimeRecover {
		get {
			return timeRecover;
		}
	}

	public TargetingRule TargetRule {
		get {
			return targetRule;
		}
	}

	public CombatRound[] CombatRounds {
		get {
			return combatRounds;
		}
	}	
}
