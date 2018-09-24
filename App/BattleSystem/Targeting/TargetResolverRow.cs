using App.BattleSystem.Entity;
using App.Core.Characters;
using App.Core.Skills;
using System;
using System.Collections;
using System.Collections.Generic;

namespace App.BattleSystem.Targeting
{
    public class TargetResolverRow : ITargetResolver
    {
        // only used when the target is single
        private PCCharacter.RowPosition rowPosition;
        private BattleEntityManager battleEntityManager;

        /// <summary>
        /// For single targets
        /// </summary>
        /// <param name="targetEnum">Target enum.</param>
        /// <param name="entityManager">Entity manager.</param>
        public TargetResolverRow(PCCharacter.RowPosition rowPosition, BattleEntityManager manager)
        {
            this.rowPosition = rowPosition;
            battleEntityManager = manager;
        }

        public bool HasValidTargets(ICombatSkill skill)
        {
            foreach (BattleEntity entity in battleEntityManager.GetRow(rowPosition))
            {
                if (skill.TargetRule.IsValidTarget(entity))
                {
                    return true;
                }
            }
            return false;
        }

        public BattleEntity[] GetTargets(ICombatSkill skill)
        {
            List<BattleEntity> filteredEntities = new List<BattleEntity>();
            foreach (BattleEntity entity in battleEntityManager.GetRow(rowPosition))
            {
                if (skill.TargetRule.IsValidTarget(entity))
                {
                    filteredEntities.Add(entity);
                }
            }
            return filteredEntities.ToArray();
        }
    } 
}


