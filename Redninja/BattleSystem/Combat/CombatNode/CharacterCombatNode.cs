using Redninja.Core.Characters;

namespace Redninja.BattleSystem.Combat.CombatNode
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

