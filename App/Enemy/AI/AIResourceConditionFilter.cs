using System;
using UnityEngine;
using System.Collections.Generic;

public class AIResourceConditionFilter : IAIFilter
{
	private AISkillRule.ResourceCondition mHpCondition;
	private float mHpPercValue;

	public AIResourceConditionFilter (AISkillRule.ResourceCondition condition, float hpPercent)
	{
		mHpCondition = condition;
		mHpPercValue = hpPercent / 100f;
	}

	public void FilterEntities (BattleEntity sourceEntity, HashSet<BattleEntity> entities)
	{
		switch(mHpCondition) {
		case AISkillRule.ResourceCondition.RES_EMPTY:
			entities.RemoveWhere(delegate(BattleEntity obj) {
				return obj.character.curResource > 0;
			});
			break;
		case AISkillRule.ResourceCondition.RES_GT:
			entities.RemoveWhere(delegate(BattleEntity obj) {
				return obj.character.curResource / obj.character.maxResource <= mHpPercValue;
			});
			break;
		case AISkillRule.ResourceCondition.RES_LT:
			entities.RemoveWhere(delegate(BattleEntity obj) {
				return obj.character.curResource / obj.character.maxResource >= mHpPercValue;
			});
			break;
			// highest, just find the max
		case AISkillRule.ResourceCondition.RES_HIGHEST:
			float maxResource = -1;
			foreach(BattleEntity entity in entities) {
				maxResource = Mathf.Max(maxResource, entity.character.curResource);
			}
			entities.RemoveWhere(delegate(BattleEntity obj) {
				return obj.character.curResource != maxResource;
			});
			break;
		case AISkillRule.ResourceCondition.RES_LOWEST:
			float minResource = 9999999;
			foreach(BattleEntity entity in entities) {
				// we want to make sure we dont count dead people
				minResource = Mathf.Min(minResource, entity.character.curResource);
			}
			entities.RemoveWhere(delegate(BattleEntity obj) {
				return obj.character.curResource != minResource;
			});
			break;		
		}
	}
}


