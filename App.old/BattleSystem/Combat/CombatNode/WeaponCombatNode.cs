using App.Core.Equipment;

namespace App.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// Combat node that represents a weapon.
    /// </summary>
    public class WeaponCombatNode : EquipmentCombatNode
    {
        private IWeapon weapon;

        /// <summary>
        /// if weapon is active weapon, we will include the damage scaling stats
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="isActiveWeapon"></param>
        public WeaponCombatNode(IWeapon weapon, bool isActiveWeapon) : base(weapon)
        {
            this.weapon = weapon;

            // 
            if (isActiveWeapon)
            {
                LoadAttributeScalar(this.weapon.AttributeScaling);
                LoadDamageMin(this.weapon.DamageMin);
                LoadDamageMax(this.weapon.DamageMax);
            }
        }
    } 
}

