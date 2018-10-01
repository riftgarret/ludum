using Redninja.BattleSystem.Entity;
using Redninja.BattleSystem.Targeting.Conditions;
using Redninja.Core.Skills;
using System;
using UnityEngine;

namespace Redninja.BattleSystem.Targeting
{
    [Serializable]
    public class TargetingRule
    {
        /// <summary>
        ///  is this single target selection or?
        /// </summary>
        public TargetingType primaryTargetType = TargetingType.SINGLE;

        /// <summary>
        /// is the default target should be the enemy or ourselves, useful for PC, not used for AI
        /// </summary>
        public TargetStart initialTarget = TargetStart.ENEMY;

        /// 
        public TargetConditionSO[] conditions = new TargetConditionSO[1];

        /// <summary>
        /// Determines whether this instance is valid target the specified entity.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid target the specified entity; otherwise, <c>false</c>.</returns>
        /// <param name="entity">Entity.</param>
        public bool IsValidTarget(BattleEntity entity)
        {
            foreach (TargetConditionSO condition in conditions)
            {
                if (!condition.IsValidTarget(entity))
                {
                    return false;
                }
            }
            return true;
        }
    } 
}


