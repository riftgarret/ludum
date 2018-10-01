using Redninja.BattleSystem.Entity;
using Redninja.Core.Skills;
using System.Collections.Generic;

namespace Redninja.BattleSystem.Targeting
{
    public interface ITargetResolver
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ITargetResolver"/> is valid target.
        /// </summary>
        /// <value><c>true</c> if is valid target; otherwise, <c>false</c>.</value>
        bool HasValidTargets(ICombatSkill skill);

        IEnumerable<BattleEntity> GetTargets(ICombatSkill skill);
    } 
}
