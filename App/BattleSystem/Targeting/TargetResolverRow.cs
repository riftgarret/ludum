using App.BattleSystem.Entity;
using App.Core.Characters;
using App.Core.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace App.BattleSystem.Targeting
{
    public class TargetResolverRow : ITargetResolver
    {
        private IEnumerable<BattleEntity> targetEntities;

        /// <summary>
        /// For single targets
        /// </summary>
        /// <param name="targetEnum">Target enum.</param>
        /// <param name="entityManager">Entity manager.</param>
        public TargetResolverRow(PCCharacter.RowPosition rowPosition, BattleEntityManager manager)
        {
            targetEntities = manager.GetRow(rowPosition);            
        }

        public bool HasValidTargets(ICombatSkill skill) => GetTargets(skill).Count() > 0;

        public IEnumerable<BattleEntity> GetTargets(ICombatSkill skill)
            => targetEntities.Where(e => skill.TargetRule.IsValidTarget(e));       
    } 
}


