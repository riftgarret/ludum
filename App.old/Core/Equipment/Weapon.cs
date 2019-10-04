using App.Core.Stats;
using System;
using UnityEngine;


namespace App.Core.Equipment
{
    // TODO separate PhysicalWeapon from MagicalWeapon (for caster reasons)
    [Serializable]
    public class Weapon : Equipment, IWeapon
    {
        public static readonly Weapon EMPTY_WEAPON = new Weapon();
        /// <summary>
        /// Not to be mistaken with EMPTY weapon, unarmed is default for first weapon slot if none
        /// </summary>
        public static readonly Weapon UNARMED_WEAPON = new Weapon();

        // weapon config to setup in the data module

        public Weapon()
        {
            WeaponType = WeaponType.AXE;
        }


        public WeaponType WeaponType
        {
            get;
            set;
        }

        public ElementVector DamageMin
        {
            get { return new ElementVector(); }
        }

        public ElementVector DamageMax
        {
            get { return new ElementVector(); }
        }

        public AttributeVector AttributeScaling
        {
            get { return new AttributeVector(); }
        }
    }

}

