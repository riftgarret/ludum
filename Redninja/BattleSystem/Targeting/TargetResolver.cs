using Redninja.BattleSystem.Entity;
using Redninja.Core.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.BattleSystem.Targeting
{
    public class TargetResolver : ITargetResolver
    {
        public delegate IEnumerable<BattleEntity> GetEntityTargets();

        private GetEntityTargets getEntityTargetDelegate;
        private IEnumerable<BattleEntity> TargetEntities => getEntityTargetDelegate.Invoke();
        
        /// <summary>
        /// Create a target resolver. Passing in a delegate in case this resolver
        /// needs to be assessed later when the game state may change.
        /// </summary>
        /// <param name="entityTargetDelegate"></param>
        public TargetResolver(GetEntityTargets entityTargetDelegate)
        {
            getEntityTargetDelegate = entityTargetDelegate;            
        }

        public bool HasValidTargets(ICombatSkill skill) => GetTargets(skill).Count() > 0;

        public IEnumerable<BattleEntity> GetTargets(ICombatSkill skill)
            => TargetEntities.Where(e => skill.TargetRule.IsValidTarget(e));
    } 
}