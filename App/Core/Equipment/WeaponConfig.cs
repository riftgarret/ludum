using System;
using System.Collections.Generic;

namespace App.Core.Equipment
{
    public class WeaponConfig
    {
        public int maxWeaponCount
        {
            get;
            private set;
        }

        public int equipedWeaponCount
        {
            get
            {
                return mEquipedWeapons.Length;
            }
        }

        private IWeapon[] mWeaponSlots;
        private IWeapon[] mEquipedWeapons;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponSlot"/> class. This will create all slots as EMPTY_WEAPON
        /// for sake of using non-null weapons in weapon skill evaluation
        /// </summary>
        /// <param name="maxWeaponCount">Max weapon count.</param>
        public WeaponConfig(int maxWeaponCount)
        {
            if (maxWeaponCount <= 0)
            {
                maxWeaponCount = 1; // must have at least 1 weapon
            }

            this.maxWeaponCount = maxWeaponCount;

            mWeaponSlots = new IWeapon[maxWeaponCount];
            mWeaponSlots[0] = Weapon.UNARMED_WEAPON;
            for (int i = 1; i < maxWeaponCount; i++)
            {
                mWeaponSlots[i] = Weapon.EMPTY_WEAPON;
            }

            RebuildEquipedArray();
        }

        /// <summary>
        /// Equips the weapon.
        /// </summary>
        /// <param name="weapon">Weapon.</param>
        /// <param name="weaponIndex">Weapon index.</param>
        public void EquipWeapon(IWeapon weapon, int weaponIndex)
        {
            if (weaponIndex >= mWeaponSlots.Length)
            {
                return; // ignore if we are out of bounds, this will really only happen on loading a config
            }

            if (weapon == null)
            {
                weapon = Weapon.EMPTY_WEAPON;
            }

            mWeaponSlots[weaponIndex] = weapon;
            RebuildEquipedArray();
        }

        /// <summary>
        /// Rebuilds the equiped array.
        /// </summary>
        private void RebuildEquipedArray()
        {

            List<IWeapon> newEquipedWeapons = new List<IWeapon>();

            foreach (IWeapon weapon in mWeaponSlots)
            {
                if (weapon != Weapon.EMPTY_WEAPON)
                {
                    newEquipedWeapons.Add(weapon);
                }
            }

            mEquipedWeapons = newEquipedWeapons.ToArray();
        }

        public IWeapon[] equipedWeapons
        {
            get { return mEquipedWeapons; }
        }

        /// <summary>
        /// Gets or sets the <see cref="CompositeWeaponSlot"/> at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public IWeapon this[int index]
        {
            get
            {
                return mWeaponSlots[index];
            }

            set
            {
                EquipWeapon(value, index);
            }
        }
    } 
}


