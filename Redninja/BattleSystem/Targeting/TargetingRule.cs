using Redninja.BattleSystem.Entities;
using Redninja.BattleSystem.Targeting.Conditions;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.BattleSystem.Targeting
{
    /// <summary>
    /// Targeting Rule represents a collection of conditions and target type.
    /// </summary>
    public class TargetingRule
    {           
        public TargetType TargetType { get; private set; }

        public ITargetPattern TargetPattern { get; private set; }

        private IEnumerable<ITargetCondition> conditions;
        
        public TargetingRule(TargetType targetType, ITargetPattern targetPattern, IEnumerable<ITargetCondition> conditions)
        {
            TargetType = targetType;
            TargetPattern = targetPattern;
            this.conditions = conditions;
        }

        /// <summary>
        /// Determines whether this instance is valid target the specified entity.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid target the specified entity; otherwise, <c>false</c>.</returns>
        /// <param name="entity">Entity.</param>
        public bool IsValidTarget(BattleEntity entity)
            => conditions.Where(c => c.IsValidTarget(entity)).Count() > 0;        
    }
}
