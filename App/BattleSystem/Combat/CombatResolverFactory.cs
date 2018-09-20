using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CombatResolverFactory {
    public static CombatResolver CreateDestination(BattleEntity entity) {
        return new CombatResolver(entity, entity.CreateDefaultCombatNode());
    }

    public static CombatResolver CreateSource(BattleEntity entity, CombatRound round) {
        return entity.CreateCombatNodeBuilder()            
            .SetSkillCombatNode(round)
            .SetWeaponIndex(round.weaponIndex)
            .BuildResolver();
    }    
}

