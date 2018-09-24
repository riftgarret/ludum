using App.BattleSystem.Entity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace App.BattleSystem.Targeting
{
    public class TargetResolverSingle : ITargetResolver
    {
        // only used when the target is single
        // TODO (why after the years is this an array lol)
        private BattleEntity[] targetEntity;

        /// <summary>
        /// For single targets
        /// </summary>
        /// <param name="targetEnum">Target enum.</param>
        /// <param name="entityManager">Entity manager.</param>
        public TargetResolverSingle(BattleEntity entity)
        {
            this.targetEntity = new BattleEntity[] { entity };
        }

        public bool HasValidTargets(ICombatSkill skill)
        {
            return skill.TargetRule.IsValidTarget(targetEntity[0]);
        }

        public BattleEntity[] GetTargets(ICombatSkill skill)
        {
            if (HasValidTargets(skill))
            {
                return targetEntity;
            }
            return new BattleEntity[0];
        }
    }

}
