using Redninja.Util;
using System;

namespace Redninja.BattleSystem.Combat.CombatNode
{    
    /// <summary>
    /// This combat property contains a combat propety type and value modifier to Redninja.y dammages.
    /// </summary>
    [Serializable]
    public class CombatProperty : ValueModifier
    {
        // TODO rice, is this ok as public
        public CombatPropertyType propertyType;

        public CombatProperty() : base()
        {
            propertyType = CombatPropertyType.NONE;
        }

    } 
}


