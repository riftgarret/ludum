using System;
using System.Collections.Generic;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions.AI
{
	public interface IAISkillRule
	{
		IEnumerable<IAITargetCondition> TargetConditions { get; }
		IEnumerable<Tuple<IAITargetPriority, ISkill>> SkillAssignments { get; }
		TargetTeam TargetType { get; }
	}
}
