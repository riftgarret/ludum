using System;
using UnityEngine;
using System.Collections.Generic;

public class AIHipointConditionFilter : IAIFilter
{
	private AISkillRule.HitPointCondition mHpCondition;
	private float mHpPercValue;

	public AIHipointConditionFilter (AISkillRule.HitPointCondition condition, float hpPercent)
	{
		mHpCondition = condition;
		mHpPercValue = hpPercent / 100f;
	}

	public void FilterEntities (BattleEntity sourceEntity, HashSet<BattleEntity> entities)
	{
		switch(mHpCondition) {
		case AISkillRule.HitPointCondition.HP_DEAD:
			entities.RemoveWhere(delegate(BattleEntity obj) {
				return obj.character.curHP > 0;
			});
			break;
		case AISkillRule.HitPointCondition.HP_GT:
			entities.RemoveWhere(delegate(BattleEntity obj) {
				return obj.character.curHP / obj.character.maxHP <= mHpPercValue;
			});
			break;
		case AISkillRule.HitPointCondition.HP_LT:
			entities.RemoveWhere(delegate(BattleEntity obj) {
				return obj.character.curHP / obj.character.maxHP >= mHpPercValue;
			});
			break;
			// highest, just find the max
		case AISkillRule.HitPointCondition.HP_HIGHEST:
			float maxHP = -1;
			foreach(BattleEntity entity in entities) {
				maxHP = Mathf.Max(maxHP, entity.character.curHP);
			}
			entities.RemoveWhere(delegate(BattleEntity obj) {
				return obj.character.curHP != maxHP;
			});
			break;
		case AISkillRule.HitPointCondition.HP_LOWEST:
			float minHP = 9999999;
			foreach(BattleEntity entity in entities) {
				// we want to make sure we dont count dead people
				minHP = Mathf.Max(1, Mathf.Min(minHP, entity.character.curHP));
			}
			entities.RemoveWhere(delegate(BattleEntity obj) {
				return obj.character.curHP != minHP;
			});
			break;		
		}
	}
}


