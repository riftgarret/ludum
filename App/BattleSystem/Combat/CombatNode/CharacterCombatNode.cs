using App.Core.Characters;

namespace App.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// Character Node.
    /// </summary>
    public class CharacterCombatNode : ConfigurableCombatNode
    {
        public CharacterCombatNode(Character c) : base()
        {
            LoadAttribute(c.attributes);
            LoadElementDefense(c.elementDefense);
            LoadElementAttack(c.elementAttack);
            LoadCombatStats(c.combatStats);
        }
    } 
}

