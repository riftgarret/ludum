using UnityEngine;
using System.Collections;

public class BattleAction : IBattleAction {		

	// time it takes to prepare, configured and set
	public float timePrepare { get { return combatSkill.TimePrepare; } } 
	public float timeAction { get { return combatSkill.TimeAction; } }  
	public float timeRecover { get { return combatSkill.TimeRecover; } } 

	public readonly ICombatSkill combatSkill;

	private int mCombatRoundCount;
	private int mCombatRoundIndex;
			
	/// <summary>
	/// The target entity. This may be null if we are targeting a group.
	/// </summary>
	public readonly ITargetResolver targetResolver;

	public readonly BattleEntity sourceEntity;
	
	public BattleAction(ICombatSkill skill, BattleEntity sourceEntity, ITargetResolver targetResolver) {
		this.combatSkill = skill;
		this.sourceEntity = sourceEntity;
		this.targetResolver = targetResolver;
		this.mCombatRoundIndex = 0;
		this.mCombatRoundCount = skill.CombatRounds.Length;
	}

	/// <summary>
	/// Important to note action clock should always be called even when the delta time has passed.
	/// the action time threshold, it will be called one last time
	/// </summary>
	/// <param name="actionClock">Action clock.</param>
	public void OnExecuteAction(float actionClock) {
		float triggerValue = timeAction *(float)(mCombatRoundIndex + 1) / (float)mCombatRoundCount;

		// if we have passed next click for the action clock
		if(triggerValue <= actionClock) {
			DoCombatRound(mCombatRoundIndex);
			mCombatRoundIndex++;
		}
	}

	/// <summary>
	/// Dos the combat round. For each target, create a execution of attack or action
	/// </summary>
	/// <param name="index">Index.</param>
	private void DoCombatRound(int index) {		
		foreach(BattleEntity targetEntity in targetResolver.GetTargets(combatSkill)) {
            ICombatOperation combatOperation = CombatOperationFactory.createOperation(sourceEntity, targetEntity, combatSkill.CombatRounds[index]);
            BattleSystem.Instance.ExecuteCombat(combatOperation);
		}
	}


	/// <summary>
	/// To complete action, not useful in current stage.
	/// </summary>
	/// <value>The total time.</value>
	public float TotalTime {
		get {
			return timeAction + timePrepare + timeRecover;
		}
	}
}
