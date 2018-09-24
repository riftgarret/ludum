
using App.BattleSystem.Effects;

namespace App.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// Combat node that represents a status effect.
    /// </summary>
    public class StatusEffectCombatNode : ConfigurableCombatNode
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusEffectCombatNode"/> class.
        /// </summary>
        /// <param name="statusEffectExecutor">Status effect executor.</param>
        protected StatusEffectCombatNode(StatusEffectRunner statusEffect) : base()
        {

        }
    }
}

