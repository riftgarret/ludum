using App.BattleSystem.Entity;
using App.BattleSystem.Targeting;
using System;

namespace App.BattleSystem.Action
{
    public class BattleActionFactory
    {
        public static BattleAction CreateBattleAction(ICombatSkill fromSkill, BattleEntity origin, ITargetResolver targetResolver)
        {
            return new BattleAction(fromSkill, origin, targetResolver);
        }
    } 
}


