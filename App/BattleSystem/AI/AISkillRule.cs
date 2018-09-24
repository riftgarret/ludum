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
        private float weight = 1f;
        public float Weight
        {
            get { return weight; }
        }

        [SerializeField]
        private CombatSkillSO mSkill = null;
        public CombatSkillSO skill
        {
            get { return mSkill; }
        }

        [SerializeField]
        private ConditionTarget mConditionTarget = ConditionTarget.PC;

        [SerializeField]
        private ConditionResolveTarget mResolvedTarget = ConditionResolveTarget.TARGET;
        public ConditionResolveTarget resolvedTarget
        {
            get { return mResolvedTarget; }
        }

        [SerializeField]
        private ConditionType mConditionType = ConditionType.ANY;

        [SerializeField]
        private ClassCondition mClassCondition = ClassCondition.CLASS_FIGHTER;

        [SerializeField]
        private HitPointCondition mHitpointCondition = HitPointCondition.HP_HIGHEST;

        [SerializeField]
        private ResourceCondition mResourceCondition = ResourceCondition.RES_HIGHEST;

        [SerializeField]
        private RowCondition mRowCondition = RowCondition.BACK_COUNT_GT;

        [SerializeField]
        private StatusCondition mStatusCondition = StatusCondition.BUFF_COUNT_GT;

        [SerializeField]
        private PartyCondition mPartyCondition = PartyCondition.PARTY_COUNT_GT;

        [SerializeField]
        private float mConditionValue = 0f;

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
            switch (mConditionType)
            {
                case ConditionType.ANY:
                    return new AIAcceptAllFilter();
                case ConditionType.CLASS:
                    return new AIClassConditionFilter(mClassCondition);
                case ConditionType.HP:
                    return new AIHipointConditionFilter(mHitpointCondition, mConditionValue);
                case ConditionType.PARTY:
                    return new AIPartyConditionFilter(mPartyCondition, (int)mConditionValue);
                case ConditionType.RES:
                    return new AIResourceConditionFilter(mResourceCondition, mConditionValue);
                case ConditionType.ROW:
                    return new AIRowConditionFilter(mRowCondition, (int)mConditionValue);
                case ConditionType.STATUS:
                    if (mStatusCondition == StatusCondition.BUFF_COUNT_GT)
                    {
                        // TEMP to stop compiler warnings
                    }
                    return new AIAcceptAllFilter();
            }
            return null;
        }

        public IAIFilter CreateTargetFilter()
        {
            return new AITargetFilter(mConditionTarget);
        }

    } 
}


