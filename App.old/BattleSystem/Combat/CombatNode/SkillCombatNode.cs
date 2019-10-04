using App.Core.Skills;

namespace App.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// A combat node that represents a skill.
    /// </summary>
    public class SkillCombatNode : ConfigurableCombatNode
    {

        /// <summary>
        /// Creates a new instance that just keeps track of some of the meta data.
        /// The skill itself should generate its own statistics because it can vary from
        /// skill level which isnt put into place.
        /// </summary>
        /// <param name="skillOrigin">Skill origin.</param>
        public SkillCombatNode(CombatRound round) : base()
        {
            Load(round.combatProperties);
        }
    } 
}

