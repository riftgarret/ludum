using App.Core.Equipment.Restrictions;
using App.Core.Stats;
using App.Util;
using System;
using UnityEngine;

namespace App.Core.Equipment
{
    public abstract class EquipmentSO : SanitySO, IEquipment
    {
        public string displayName;
        public Texture2D icon;

        // elemental offense and defense abilities
        public ElementVector offenseScalar = new ElementVector();
        public ElementVector defenseScalar = new ElementVector();

        // attribute bonuses
        public AttributeVector attributeExtra = new AttributeVector();

        // allow user to wear
        public EquipmentRestriction equipmentRestriction = new EquipmentRestriction();

        protected override void SanityCheck()
        {
            if (string.IsNullOrEmpty(displayName))
            {
                LogNull("displayName");
            }

            if (icon == null)
            {
                LogNull("icon");
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public Texture2D Icon
        {
            get
            {
                return icon;
            }
        }

        public ElementVector OffensiveScalar
        {
            get { return offenseScalar; }
        }

        public ElementVector DefensiveScalar
        {
            get { return defenseScalar; }
        }

        public AttributeVector AttributeExtra
        {
            get { return attributeExtra; }
        }
    } 
}


