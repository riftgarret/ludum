using App.Util;
using System;

namespace App.BattleSystem.Combat.CombatNode
{    
    /// <summary>
    /// This combat property contains a combat propety type and value modifier to apply dammages.
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


