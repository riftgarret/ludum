using App.BattleSystem.Combat.CombatNode;
using System;

namespace App.Core.Equipment
{
    [Serializable]
    public class Armor : Equipment, IArmor
    {
        public static readonly Armor EMPTY_ARMOR = new Armor();

        public ArmorPosition ArmorPosition
        {
            private set;
            get;
        }

        public ArmorType ArmorType
        {
            private set;
            get;
        }

        public CombatProperty[] CombatProperties
        {
            private set;
            get;
        }

    } 
}

