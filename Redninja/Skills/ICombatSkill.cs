using Davfalcon;
using Redninja.Actions;
using Redninja.Targeting;
using System.Collections.Generic;

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

        // TODO list? or something to spcify the amount of time required between rounds?
        List<CombatRound> CombatRounds { get; }

        // TODO figure out how to capture combat parameters
    }
}
