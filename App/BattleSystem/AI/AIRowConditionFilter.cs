using App.BattleSystem.Entity;
using App.Core.Characters;
using System;
using System.Collections.Generic;

namespace App.BattleSystem.AI
{
    public class AIRowConditionFilter : IAIFilter
    {
        private AISkillRule.RowCondition mRowCondition;
        private int mFilterCount;

        public AIRowConditionFilter(AISkillRule.RowCondition rowCondition, int count)
        {
            mRowCondition = rowCondition;
            mFilterCount = count;
        }

        // filter out by if targets are within range
        public void FilterEntities(BattleEntity sourceEntity, HashSet<BattleEntity> entities)
        {
            // first row filter
            PCCharacter.RowPosition rowPosition = PCCharacter.RowPosition.FRONT;
            switch (mRowCondition)
            {
                case AISkillRule.RowCondition.BACK_COUNT_GT:
                case AISkillRule.RowCondition.BACK_COUNT_LT:
                    rowPosition = PCCharacter.RowPosition.BACK;
                    break;
                case AISkillRule.RowCondition.FRONT_COUNT_GT:
                case AISkillRule.RowCondition.FRONT_COUNT_LT:
                    rowPosition = PCCharacter.RowPosition.FRONT;
                    break;
                case AISkillRule.RowCondition.MIDDLE_COUNT_GT:
                case AISkillRule.RowCondition.MIDDLE_COUNT_LT:
                    rowPosition = PCCharacter.RowPosition.MIDDLE;
                    break;
            }

            // then remove all entries are not that row
            entities.RemoveWhere(delegate (BattleEntity obj)
            {
                if (obj is PCBattleEntity && ((PCBattleEntity)obj).pcCharacter.rowPosition == rowPosition)
                {
                    return false;
                }
                return true;
            });

            // finally see if it meets the condition, if it does, leave them, if it doesnt, remove all
            switch (mRowCondition)
            {
                case AISkillRule.RowCondition.BACK_COUNT_GT:
                case AISkillRule.RowCondition.FRONT_COUNT_GT:
                case AISkillRule.RowCondition.MIDDLE_COUNT_GT:
                    if (entities.Count > mFilterCount)
                    {
                        // ok
                    }
                    else
                    {
                        entities.Clear();
                    }
                    break;
                case AISkillRule.RowCondition.BACK_COUNT_LT:
                case AISkillRule.RowCondition.FRONT_COUNT_LT:
                case AISkillRule.RowCondition.MIDDLE_COUNT_LT:
                    if (entities.Count < mFilterCount)
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