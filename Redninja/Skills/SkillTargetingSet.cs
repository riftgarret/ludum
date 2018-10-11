using System.Collections.Generic;
using System.Linq;
using Davfalcon.Revelator;
using Redninja.Targeting;
using static Redninja.Skills.CombatRound;

namespace Redninja.Skills
{
	public class SkillTargetingSet
	{
		public ITargetingRule TargetingRule { get; private set; }
		public IEnumerable<CombatRound> Rounds { get; private set; }

		public IEnumerable<SkillResolver> GetSkillResolvers(ISelectedTarget target)
		{
			if (TargetingRule.Type == TargetType.Pattern)
			{
				return Rounds.Select(round => round.GetResolver(new SelectedTargetPattern(TargetingRule, round.Pattern ?? TargetingRule.Pattern, target.Team, target.Anchor)));
			}
			else return Rounds.Select(round => round.GetResolver(target));
		}

		public class Builder : BuilderBase<SkillTargetingSet>
		{
			private readonly ITargetingRule targetingRule;
			private List<CombatRound> rounds;

			public Builder(ITargetingRule targetingRule)
			{
				this.targetingRule = targetingRule;
				Reset();
			}

			public Builder Reset()
			{
				rounds = new List<CombatRound>();
				build = new SkillTargetingSet()
				{
					TargetingRule = targetingRule,
					Rounds = rounds.AsReadOnly()
				};
				return this;
			}

			public Builder AddCombatRound(float executionStart, OperationProvider getOperation)
				=> AddCombatRound(new CombatRound(executionStart, getOperation));

			public Builder AddCombatRound(float executionStart, ITargetPattern pattern, OperationProvider getOperation)
				=> AddCombatRound(new CombatRound(executionStart, pattern, getOperation));

			public Builder AddCombatRound(CombatRound round)
			{
				rounds.Add(round);
				return this;
			}
		}
	}
}
