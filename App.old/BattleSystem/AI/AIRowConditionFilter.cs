using App.BattleSystem.Entity;
using App.Core.Characters;
using System;
using System.Collections.Generic;

namespace App.BattleSystem.AI
{
    public class AIRowConditionFilter : IAIFilter
    {
        private AISkillRule.RowCondition rowCondition;
        private int targetRow;
        private int filterCount;

        public AIRowConditionFilter(AISkillRule.RowCondition rowCondition, int count, int targetRow)
        {
            this.rowCondition = rowCondition;
            filterCount = count;
            this.targetRow = targetRow;
        }

        // filter out by if targets are within range
        public void FilterEntities(BattleEntity sourceEntity, HashSet<BattleEntity> entities)
        {           
            // then remove all entries are not that row           
            entities.RemoveWhere(ent => ent.Position.Row != targetRow);

            // finally see if it meets the condition, if it does, leave them, if it doesnt, remove all
            switch (rowCondition)
            {
                case AISkillRule.RowCondition.ROW_COUNT_GT:                
                    if (entities.Count > filterCount)
                    {
                        // ok
                    }
                    else
                    {
                        entities.Clear();
                    }
                    break;
                case AISkillRule.RowCondition.ROW_COUNT_LT:
                    if (entities.Count < filterCount)
                    {
                        // ok
                    }
                    else
                    {
                        entities.Clear();
                    }
                    break;
            }
        }
    } 
}