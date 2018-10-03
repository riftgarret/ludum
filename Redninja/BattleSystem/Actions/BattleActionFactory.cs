using Redninja.BattleSystem.Entities;
using Redninja.BattleSystem.Targeting;
using Redninja.Core.Skills;
using System;

namespace Redninja.BattleSystem.Actions
{
    public class BattleActionFactory
    {
        public static BattleActionSkill CreateBattleAction(ICombatSkill fromSkill, BattleEntity origin, ITargetResolver targetResolver)
        {
            return new BattleActionSkill(fromSkill, origin, targetResolver);
        }
    } 
}


