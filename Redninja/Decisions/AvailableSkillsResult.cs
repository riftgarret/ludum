using Redninja.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Decisions
{
    /// <summary>
    /// Result for requesting what available skills a entity can use.
    /// </summary>
    public class AvailableSkillsResult
    {
        public IEnumerable<ICombatSkill> Skills { get; }
        public IBattleEntity Entity { get; }
        private DecisionManager manager;

        internal AvailableSkillsResult(DecisionManager manager,
            IBattleEntity entity,
            IEnumerable<ICombatSkill> skills)
        {
            Skills = skills;
            Entity = entity;
            this.manager = manager;
        }

        public AvailableTargetsResult GetAvailableTargets(ICombatSkill skill)
            => manager.GetSelectableTargets(Entity, skill);
    }
}
