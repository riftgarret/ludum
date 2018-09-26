using App.BattleSystem.Entity;
using App.BattleSystem.Targeting;
using App.Core.Skills;
using System;

namespace App.BattleSystem.Actions
{
    public class BattleActionFactory
    {
        public static BattleActionSkill CreateBattleAction(ICombatSkill fromSkill, BattleEntity origin, ITargetResolver targetResolver)
        {
            return new BattleActionSkill(fromSkill, origin, targetResolver);
        }
    } 
}


