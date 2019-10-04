using System;
using UnityEngine;
using System.Collections.Generic;
using App.BattleSystem.Entity;

namespace App.BattleSystem.AI
{

    /// <summary>
    /// TODO filter this to be about resource instead of HPs (mana or other resource)
    /// </summary>
    public class AIResourceConditionFilter : IAIFilter
    {
        private AISkillRule.ResourceCondition hpCondition;
        private float hpPercValue;

        public AIResourceConditionFilter(AISkillRule.ResourceCondition condition, float hpPercent)
        {
            hpCondition = condition;
            hpPercValue = hpPercent / 100f;
        }

        public void FilterEntities(BattleEntity sourceEntity, HashSet<BattleEntity> entities)
        {
            switch (hpCondition)
            {
                case AISkillRule.ResourceCondition.RES_EMPTY:
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return obj.Character.curResource > 0;
                    });
                    break;
                case AISkillRule.ResourceCondition.RES_GT:
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return obj.Character.curResource / obj.Character.MaxResource <= hpPercValue;
                    });
                    break;
                case AISkillRule.ResourceCondition.RES_LT:
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return obj.Character.curResource / obj.Character.MaxResource >= hpPercValue;
                    });
                    break;
                // highest, just find the max
                case AISkillRule.ResourceCondition.RES_HIGHEST:
                    float maxResource = -1;
                    foreach (BattleEntity entity in entities)
                    {
                        maxResource = Mathf.Max(maxResource, entity.Character.curResource);
                    }
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return obj.Character.curResource != maxResource;
                    });
                    break;
                case AISkillRule.ResourceCondition.RES_LOWEST:
                    float minResource = 9999999;
                    foreach (BattleEntity entity in entities)
                    {
                        // we want to make sure we dont count dead people
                        minResource = Mathf.Min(minResource, entity.Character.curResource);
                    }
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return obj.Character.curResource != minResource;
                    });
                    break;
            }
        }
    }

}

