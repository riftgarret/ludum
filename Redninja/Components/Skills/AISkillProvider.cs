using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Actions;
using Redninja.Components.Decisions.AI;
using System.Linq;

namespace Redninja.Components.Skills
{
	public class AISkillProvider : ISkillProvider
	{
		private List<ISkill> Skills { get; } = new List<ISkill>();
		// TODO figure out attack time
		private ActionTime AttackTime { get; set; } = new ActionTime(1, 1, 1);

		public AISkillProvider(AIRuleSet aiBehavior)
		{
			Skills.AddRange(
				aiBehavior.Rules
				.Where(rule => rule is IAISkillRule)
				.SelectMany(rule => ((IAISkillRule)rule).SkillAssignments)
				.Select(skillAssignment => skillAssignment.Item2)
				.Distinct()
			);			
		}

		public IEnumerable<ISkill> GetSkills() => Skills;
	}
}
