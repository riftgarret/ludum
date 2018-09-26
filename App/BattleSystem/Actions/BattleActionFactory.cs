using App.BattleSystem.Entity;
using App.BattleSystem.Targeting;
using App.Core.Skills;
using System;

namespace App.BattleSystem.Actions
{
    public class BattleActionFactory
    {
        public static BattleActioSkill CreateBattleAction(ICombatSkill fromSkill, BattleEntity origin, ITargetResolver targetResolver)
        {
            return new BattleActioSkill(fromSkill, origin, targetResolver);
        }
    } 
}


