using Redninja.BattleSystem.Combat.CombatNode;
using Redninja.BattleSystem.Entity;
using Redninja.Core.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Redninja.BattleSystem.Combat
{
    /// <summary>
    /// Create Entity Combat Resolvers for either source (attacker) or destinations (defender)
    /// </summary>
    public class CombatResolverFactory
    {
        public static EntityCombatResolver CreateDestination(BattleEntity entity)
        {
            return new EntityCombatResolver(entity, entity.CreateDefaultCombatNode());
        }

        public static EntityCombatResolver CreateSource(BattleEntity entity, CombatRound round)
        {
            return entity.CreateCombatNodeBuilder()
                .SetSkillCombatNode(round)
                .SetWeaponIndex(round.weaponIndex)
                .BuildResolver();
        }
    } 
}

