using App.BattleSystem.Entity;
using System;
using System.Collections.Generic;

namespace App.BattleSystem.AI
{
    public class AIClassConditionFilter : IAIFilter
    {
        // class condition to compare
        private AISkillRule.ClassCondition classCondition;

        public AIClassConditionFilter(AISkillRule.ClassCondition classCondition)
        {
        }

        public void FilterEntities(BattleEntity sourceEntity, HashSet<BattleEntity> entities)
        {
            return; // TODO implement 
        }
    } 
}


