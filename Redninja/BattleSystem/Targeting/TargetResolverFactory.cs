using Redninja.BattleSystem.Entities;
using Redninja.Core.Characters;
using System;
using System.Collections.Generic;

namespace Redninja.BattleSystem.Targeting
{
    public class TargetResolverFactory
    {
        /// <summary>
        /// Creates the target resolver. This should handle turning a selected target into a resolved target
        /// </summary>
        /// <returns>The target resolver.</returns>
        /// <param name="target">Target.</param>
        /// <param name="manager">Manager.</param>
        public static ITargetResolver CreateTargetResolver(BattleEntity sourceEntity, SelectableTarget target, BattleEntityManager manager)
        {
            switch (target.resolvedTargetType)
            {
                case ResolvedTargetEnum.SINGLE:
                    return new TargetResolver(() => manager.AllEntities);
                case ResolvedTargetEnum.SELF_PATTERN:
                    throw new NotImplementedException("Row mechanics missing");                    
                case ResolvedTargetEnum.SELF:
                    return new TargetResolver(() => new BattleEntity[] { sourceEntity });
                case ResolvedTargetEnum.ENEMY_ALL:
                    return new TargetResolver(() => manager.EnemyEntities);
                case ResolvedTargetEnum.ENEMY_PATERN:
                    throw new NotImplementedException("Row mechanics missing");                    
                case ResolvedTargetEnum.ALLY_ALL:
                    return new TargetResolver(() => manager.PCEntities);
            }
            return null; // TODO turn into target classes
        }

        //public static ITargetResolver CreateTargetResolver(BattleEntity target, Com
    } 
}

