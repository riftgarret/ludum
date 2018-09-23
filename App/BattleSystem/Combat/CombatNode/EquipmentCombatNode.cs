namespace App.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// A Combat node representing equipment.
    /// </summary>
    public class EquipmentCombatNode : ConfigurableCombatNode
    {
        public EquipmentCombatNode(IEquipment equipment)
        {
            LoadElementAttackScalar(equipment.OffensiveScalar);
            LoadElementDefenseScalar(equipment.DefensiveScalar);
            LoadAttribute(equipment.AttributeExtra);
        }
    } 
}

