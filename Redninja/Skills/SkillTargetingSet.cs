using System.Collections.Generic;
using System.Linq;
using Redninja.Targeting;

namespace Redninja.Skills
{
	public class SkillTargetingSet
	{
		public ITargetingRule TargetingRule { get; }
		public IEnumerable<CombatRound> Rounds { get; }

		public IEnumerable<SkillResolver> GetSkillResolvers(ISelectedTarget target)
		{
			if (TargetingRule.Type == TargetType.Pattern)
			{
				return Rounds.Select(round => round.GetResolver(new SelectedTargetPattern(TargetingRule, round.Pattern ?? TargetingRule.Pattern, target.Team, target.Anchor)));
			}
			else return Rounds.Select(round => round.GetResolver(target));
		}
	}
}
