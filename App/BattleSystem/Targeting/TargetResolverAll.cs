using App.BattleSystem.Entity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace App.BattleSystem.Targeting
{
    public class TargetResolverAll : ITargetResolver
    {
        // only used when the target is single
        private bool isEnemy;
        private BattleEntityManager battleEntityManager;

        /// <summary>
        /// For single targets
        /// </summary>
        /// <param name="targetEnum">Target enum.</param>
        /// <param name="entityManager">Entity manager.</param>
        public TargetResolverAll(bool isEnemy, BattleEntityManager manager)
        {
            this.isEnemy = isEnemy;
            battleEntityManager = manager;
        }

        private BattleEntity[] TargetEntities
        {
            get
            {
                if (isEnemy)
                {
                    return battleEntityManager.enemyEntities;
                }
                return battleEntityManager.pcEntities;
            }
        }


        public bool HasValidTargets(ICombatSkill skill)
        {
            foreach (BattleEntity entity in TargetEntities)
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

            foreach (BattleEntity entity in TargetEntities)
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