using Davfalcon;
using Redninja.Actions;
using Redninja.Targeting;

namespace Redninja.Skills
{
    /// <summary>
    /// Implementation that should contain details of what the skill should do and
    /// target scenario.
    /// </summary>
    public interface ICombatSkill : INameable
    {
        ActionTime Time { get; }

        TargetingRule TargetRule { get; }        

        // TODO figure out how to capture combat parameters
    }
}
