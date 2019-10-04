using App.Core.Equipment;

namespace App.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// Armor Node.
    /// </summary>
    public class ArmorCombatNode : EquipmentCombatNode
    {
        public ArmorCombatNode(IArmor armor) : base(armor)
        {
        }
    } 
}

