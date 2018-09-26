using App.BattleSystem.Entity;
using App.Core.Characters;
using System;
using System.Collections.Generic;

namespace App.BattleSystem.Targeting
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
                case ResolvedTargetEnum.SELF_ROW:
                    return new TargetResolver(() => manager.GetRow(((PCCharacter) sourceEntity.Character).rowPosition));                    
                case ResolvedTargetEnum.SELF:
                    return new TargetResolver(() => new BattleEntity[] { sourceEntity });
                case ResolvedTargetEnum.ENEMY_ALL:
                    return new TargetResolver(() => manager.EnemyEntities);
                case ResolvedTargetEnum.ALLY_ROW_MIDDLE:
                    return new TargetResolver(() => manager.GetRow(PCCharacter.RowPosition.MIDDLE));                    
                case ResolvedTargetEnum.ALLY_ROW_FRONT:
                    return new TargetResolver(() => manager.GetRow(PCCharacter.RowPosition.FRONT));                    
                case ResolvedTargetEnum.ALLY_ROW_BACK:
                    return new TargetResolver(() => manager.GetRow(PCCharacter.RowPosition.BACK));
                case ResolvedTargetEnum.ALLY_ALL:
                    return new TargetResolver(() => manager.PCEntities);
            }
            return null; // TODO turn into target classes
        }

        //public static ITargetResolver CreateTargetResolver(BattleEntity target, Com
    } 
}

