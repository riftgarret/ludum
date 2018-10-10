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
			=> Rounds.Select(round => round.GetResolver(target));

		public IEnumerable<SkillResolver> GetSkillResolvers(int team, Coordinate anchor)
			=> Rounds.Select(round => round.GetResolver(new SelectedTargetPattern(TargetingRule, round.Pattern, team, anchor)));
	}
}
