using App.Core.Stats;
using System;
using UnityEngine;

namespace App.Core.Equipment
{
    [Serializable]
    public class WeaponSO : EquipmentSO, IWeapon
    {
        public ElementVector damageMin = new ElementVector();
        public ElementVector damageMax = new ElementVector();

        // weapon scaling
        public AttributeVector attributeScaling = new AttributeVector();

        public GameConstants.WeaponHand weaponHand = GameConstants.WeaponHand.MAIN;

        protected override void SanityCheck()
        {
            base.SanityCheck();
        }

        // TODO build combat node to pull values from for UI

        public ElementVector DamageMin
        {
            get { return damageMin; }
        }

        public ElementVector DamageMax
        {
            get { return damageMax; }
        }

        public AttributeVector AttributeScaling
        {
            get { return attributeScaling; }
        }
    } 
}