namespace App.BattleSystem.Combat.Operation.Result
{
    /// <summary>
    /// Status effect logic node.
    /// </summary>
    public abstract class StatusEffectLogic : BaseCombatLogic
    {

        protected StatusEffectRule rule;

        public override string ToString()
        {
            return string.Format("[StatusEffectRule: p={0}, e={1}]",
                                  rule.rule,
                                  rule.effect);
        }
    } 
}
