using App.BattleSystem.Entity;
using App.Core.Characters;
using System;
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
        public static ITargetResolver CreateTargetResolver(SelectableTarget target, BattleEntityManager manager)
        {
            switch (target.resolvedTargetType)
            {
                case ResolvedTargetEnum.SINGLE:
                    return new TargetResolver(() => manager.AllEntities);
                case ResolvedTargetEnum.SELF_ROW:
                    return TargetResolver(() => manager.AllEntities);
                    PCBattleEntity entity = (PCBattleEntity)target.entities[0];
                    return new TargetResolverRow(entity.pcCharacter.rowPosition, manager);
                case ResolvedTargetEnum.SELF:
                    return new TargetResolverSingle(target.entities[0]);
                case ResolvedTargetEnum.ENEMY_ALL:
                    return new TargetResolver(true, manager);
                case ResolvedTargetEnum.ALLY_ROW_MIDDLE:
                    return new TargetResolverRow(PCCharacter.RowPosition.MIDDLE, manager);
                case ResolvedTargetEnum.ALLY_ROW_FRONT:
                    return new TargetResolverRow(PCCharacter.RowPosition.FRONT, manager);
                case ResolvedTargetEnum.ALLY_ROW_BACK:
                    return new TargetResolverRow(PCCharacter.RowPosition.BACK, manager);
                case ResolvedTargetEnum.ALLY_ALL:
                    return new TargetResolver(false, manager);
            }
            return null; // TODO turn into target classes
        }

        //public static ITargetResolver CreateTargetResolver(BattleEntity target, Com
    } 
}

