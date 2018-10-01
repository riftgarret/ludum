using Redninja.Core.Equipment;

namespace Redninja.BattleSystem.Combat.CombatNode
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

