using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Core.Equipment
{
    public class ArmorConfig
    {
        private ArmorSlot[] m_ArmorConfiguration;

        public ArmorConfig(ArmorPosition[] allowedArmorSlots)
        {
            if (allowedArmorSlots == null)
            {
                Debug.Log("null armorSlots");
            }

            if (allowedArmorSlots.Length == 0)
            {
                Debug.Log("zero armorSlots");
            }

            m_ArmorConfiguration = new ArmorSlot[allowedArmorSlots.Length];
            for (int i = 0; i < allowedArmorSlots.Length; i++)
            {
                m_ArmorConfiguration[i] = new ArmorSlot(allowedArmorSlots[i]);
            }

        }

        public IArmor[] equipedArmor
        {
            get
            {
                List<IArmor> armor = new List<IArmor>();
                ArmorSlot[] items = m_ArmorConfiguration;
                foreach (ArmorSlot item in items)
                {
                    armor.Add(item.armor);
                }
                return armor.ToArray();
            }
        }

        public void EquipArmor(IArmor armor, int slotIndex)
        {
            if (armor == null)
            {
                armor = Armor.EMPTY_ARMOR;
            }

            ArmorSlot slot = m_ArmorConfiguration[slotIndex];
            if (armor != Armor.EMPTY_ARMOR && slot.position != armor.ArmorPosition)
            {
                Debug.Log("Invalid armor position for armor item: " + armor.DisplayName);
            }
            slot.armor = armor;
        }

        /// <summary>
        /// Gets or sets the <see cref="ArmorConfig"/> with the specified position.
        /// </summary>
        /// <param name="position">Position.</param>
        public IArmor this[int position]
        {
            get
            {
                return m_ArmorConfiguration[position].armor;
            }

            set
            {
                if (value == null)
                {
                    value = Armor.EMPTY_ARMOR;
                }
                m_ArmorConfiguration[position].armor = value;
            }
        }

        public class ArmorSlot
        {
            public ArmorPosition position { get; set; }
            public IArmor armor { get; set; }
            public ArmorSlot(ArmorPosition pos)
            {
                position = pos;
                armor = Armor.EMPTY_ARMOR;
            }
        }
    } 
}

