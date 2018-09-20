using System;
using System.Collections;
using System.Collections.Generic;

public class TargetResolverAll : ITargetResolver
{
	// only used when the target is single
	private bool mIsEnemy;
	private BattleEntityManagerComponent mBattleEntityManager;
	
	/// <summary>
	/// For single targets
	/// </summary>
	/// <param name="targetEnum">Target enum.</param>
	/// <param name="entityManager">Entity manager.</param>
	public TargetResolverAll (bool isEnemy, BattleEntityManagerComponent manager)
	{
		mIsEnemy = isEnemy;
		mBattleEntityManager = manager;
	}	

	private BattleEntity[] targetEntities {
		get {
			if(mIsEnemy) {
				return mBattleEntityManager.enemyEntities;
			}
			return mBattleEntityManager.pcEntities;
		}
	}


	public bool HasValidTargets (ICombatSkill skill)
	{
		foreach(BattleEntity entity in targetEntities) {
			if(skill.TargetRule.IsValidTarget(entity)) {
				return true;
			}
		}
		return false;
	}
	
	public BattleEntity[] GetTargets(ICombatSkill skill) {
		List<BattleEntity> filteredEntities = new List<BattleEntity>();

		foreach(BattleEntity entity in targetEntities) {					

			if(skill.TargetRule.IsValidTarget(entity)) {
				filteredEntities.Add(entity);
			}

		}
		return filteredEntities.ToArray();
	}
}