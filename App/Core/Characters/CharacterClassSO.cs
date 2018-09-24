using App.Core.Equipment;
using App.Util;
using System;
using System.Collections.Generic;

namespace App.Core.Characters
{
    [Serializable]
    public class CharacterClassSO : SanitySO
    {
        public float expertisePerLevel = 1f;
        public float hitpointsPerLevel = 10f;
        public string displayName;

        public bool hasArmorTorso = true;
        public bool hasArmorLegs = true;
        public bool hasArmorArms = true;
        public bool hasArmorHead = true;
        public bool hasArmorShield = false;

        public int maxWeaponCount = 1;

        /// <summary>
        /// Creates the weapon config.
        /// </summary>
        /// <returns>The weapon config.</returns>
        public WeaponConfig CreateWeaponConfig()
        {
            return new WeaponConfig(maxWeaponCount);
        }

        /// <summary>
        /// Creates the weapon config.
        /// </summary>
        /// <returns>The weapon config.</returns>
        public ArmorConfig CreateArmorConfig()
        {
            List<ArmorPosition> positionSet = new List<ArmorPosition>();

            // kind of in order
            if (hasArmorHead)
            {
                positionSet.Add(ArmorPosition.HEAD);
            }

            if (hasArmorTorso)
            {
                positionSet.Add(ArmorPosition.TORSO);
            }

            if (hasArmorArms)
            {
                positionSet.Add(ArmorPosition.ARMS);
            }

            if (hasArmorLegs)
            {
                positionSet.Add(ArmorPosition.LEGS);
            }

            if (hasArmorShield)
            {
                positionSet.Add(ArmorPosition.SHIELD);
            }



            return new ArmorConfig(positionSet.ToArray());
        }

        /// <summary>
        /// Calculates the hitpoints.
        /// </summary>
        /// <returns>The hitpoints.</returns>
        /// <param name="character">Character.</param>
        public float CalculateHitpoints(Character character)
        {
            return character.level * hitpointsPerLevel + (character.attributes.vitality);
        }

        /// <summary>
        /// Calculates the expertise.
        /// </summary>
        /// <returns>The expertise.</returns>
        /// <param name="character">Character.</param>
        public float CalculateExpertise(Character character)
        {
            return expertisePerLevel * character.level;
        }

        protected override void SanityCheck()
        {
            if (string.IsNullOrEmpty(displayName))
            {
                LogNull("displayName");
            }
        }
    } 
}