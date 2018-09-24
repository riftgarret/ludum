using App.BattleSystem.Entity;
using System;
using System.Collections.Generic;

/// <summary>
/// AI target filter. A class to check to make sure we are only looking at valid targets for this AI rule
/// </summary>
namespace App.BattleSystem.AI
{
    public class AITargetFilter : IAIFilter
    {
        private AISkillRule.ConditionTarget targetParam;


        public AITargetFilter(AISkillRule.ConditionTarget target)
        {
            this.targetParam = target;
        }

        public void FilterEntities(BattleEntity sourceEntity, HashSet<BattleEntity> entities)
        {
            switch (targetParam)
            {
                case AISkillRule.ConditionTarget.ENEMY:
                    entities.RemoveWhere(FilterEnemy);
                    break;
                case AISkillRule.ConditionTarget.PC:
                    entities.RemoveWhere(FilterPC);
                    break;
                case AISkillRule.ConditionTarget.SELF:
                    entities.RemoveWhere(delegate (BattleEntity obj)
                    {
                        return sourceEntity != obj;
                    });
                    break;
            }
        }

        public bool FilterPC(BattleEntity entity)
        {
            return !entity.IsPC;
        }

        public bool FilterEnemy(BattleEntity entity)
        {
            return entity.IsPC;
        }
    } 
}

