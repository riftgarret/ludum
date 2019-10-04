using System;
using UnityEngine;
using System.Collections.Generic;
using App.BattleSystem.Entity;

namespace App.BattleSystem.AI
{
    public class AIHipointConditionFilter : IAIFilter
    {
        private AISkillRule.HitPointCondition hpCondition;
        private float hpPercValue;

        public AIHipointConditionFilter(AISkillRule.HitPointCondition condition, float hpPercent)
        {
            hpCondition = condition;
            hpPercValue = hpPercent / 100f;
        }

        public void FilterEntities(BattleEntity sourceEntity, HashSet<BattleEntity> entities)
        {
            switch (hpCondition)
            {
                case AISkillRule.HitPointCondition.HP_DEAD:
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return obj.Character.curHP > 0;
                    });
                    break;
                case AISkillRule.HitPointCondition.HP_GT:
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return obj.Character.curHP / obj.Character.MaxHP <= hpPercValue;
                    });
                    break;
                case AISkillRule.HitPointCondition.HP_LT:
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return obj.Character.curHP / obj.Character.MaxHP >= hpPercValue;
                    });
                    break;
                // highest, just find the max
                case AISkillRule.HitPointCondition.HP_HIGHEST:
                    float maxHP = -1;
                    foreach (BattleEntity entity in entities)
                    {
                        maxHP = Mathf.Max(maxHP, entity.Character.curHP);
                    }
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return obj.Character.curHP != maxHP;
                    });
                    break;
                case AISkillRule.HitPointCondition.HP_LOWEST:
                    float minHP = 9999999;
                    foreach (BattleEntity entity in entities)
                    {
                        // we want to make sure we dont count dead people
                        minHP = Mathf.Max(1, Mathf.Min(minHP, entity.Character.curHP));
                    }
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return obj.Character.curHP != minHP;
                    });
                    break;
            }
        }
    } 
}


