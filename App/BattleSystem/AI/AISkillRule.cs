using UnityEngine;
using System;
using App.Core.Skills;

namespace App.BattleSystem.AI
{
    [Serializable]
    public class AISkillRule
    {
        /// <summary>
        /// The m weight of the skill being used after all available skills are evaluated
        /// </summary>
        [SerializeField]
        private float weight;
        public float Weight { get => weight; }

        [SerializeField]
        private CombatSkillSO skill;
        public CombatSkillSO Skill { get => skill; }

        [SerializeField]
        private ConditionTarget conditionTarget = ConditionTarget.PC;

        [SerializeField]        
        private ConditionResolveTarget resolvedTarget = ConditionResolveTarget.TARGET;
        public ConditionResolveTarget ResolvedTarget { get => resolvedTarget;  }        

        [SerializeField]
        private ConditionType conditionType = ConditionType.ANY;

        [SerializeField]
        private ClassCondition classCondition = ClassCondition.CLASS_FIGHTER;

        [SerializeField]
        private HitPointCondition hitpointCondition = HitPointCondition.HP_HIGHEST;

        [SerializeField]
        private ResourceCondition resourceCondition = ResourceCondition.RES_HIGHEST;

        [SerializeField]
        private RowCondition rowCondition = RowCondition.BACK_COUNT_GT;

        [SerializeField]
        private StatusCondition statusCondition = StatusCondition.BUFF_COUNT_GT;

        [SerializeField]
        private PartyCondition partyCondition = PartyCondition.PARTY_COUNT_GT;

        [SerializeField]
        private float conditionValue = 0f;

        public enum ConditionType
        {
            ANY,
            CLASS,
            ROW,
            HP,
            RES,
            STATUS,
            PARTY
        }

        public enum ConditionTarget
        {
            SELF,
            ENEMY,
            PC
        }

        public enum ConditionResolveTarget
        {
            TARGET,
            SELF
        }

        public enum ClassCondition
        {
            CLASS_FIGHTER,
            CLASS_MAGE,
            CLASS_ROGUE,
            CLASS_SQUIRE
        }

        public enum HitPointCondition
        {
            HP_GT,
            HP_LT,
            HP_HIGHEST,
            HP_LOWEST,
            HP_DEAD
        }

        public enum ResourceCondition
        {
            RES_GT,
            RES_LT,
            RES_HIGHEST,
            RES_LOWEST,
            RES_EMPTY
        }

        public enum RowCondition
        {
            FRONT_COUNT_GT,
            FRONT_COUNT_LT,
            MIDDLE_COUNT_GT,
            MIDDLE_COUNT_LT,
            BACK_COUNT_GT,
            BACK_COUNT_LT
        }

        public enum StatusCondition
        {
            DEBUFF_COUNT_GT,
            DEBUFF_COUNT_LT,
            BUFF_COUNT_GT,
            BUFF_COUNT_LT,
            SELF_BLIND,
            SELF_HOARSE
        }

        public enum PartyCondition
        {
            PARTY_COUNT_LT,
            PARTY_COUNT_GT
        }


        /// <summary>
        /// Creates the condition filter. 
        /// </summary>
        /// <returns>The condition filter.</returns>
        public IAIFilter CreateConditionFilter()
        {
            switch (conditionType)
            {
                case ConditionType.ANY:
                    return new AIAcceptAllFilter();
                case ConditionType.CLASS:
                    return new AIClassConditionFilter(classCondition);
                case ConditionType.HP:
                    return new AIHipointConditionFilter(hitpointCondition, conditionValue);
                case ConditionType.PARTY:
                    return new AIPartyConditionFilter(partyCondition, (int)conditionValue);
                case ConditionType.RES:
                    return new AIResourceConditionFilter(resourceCondition, conditionValue);
                case ConditionType.ROW:
                    return new AIRowConditionFilter(rowCondition, (int)conditionValue);
                case ConditionType.STATUS:
                    if (statusCondition == StatusCondition.BUFF_COUNT_GT)
                    {
                        // TEMP to stop compiler warnings
                    }
                    return new AIAcceptAllFilter();
            }
            return null;
        }

        public IAIFilter CreateTargetFilter()
        {
            return new AITargetFilter(conditionTarget);
        }

    } 
}


