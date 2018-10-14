using System.Collections.Generic;
using System.Linq;
using Davfalcon.Builders;
using Redninja.Targeting;
using static Redninja.Skills.SkillOperationDefinition;

namespace Redninja.Skills
{
	public class SkillTargetingSet
	{
		public ITargetingRule TargetingRule { get; private set; }
		public IEnumerable<SkillOperationDefinition> Rounds { get; private set; }

		public IEnumerable<ISkillResolver> GetSkillResolvers(ISelectedTarget target)
		{
			if (TargetingRule.Type == TargetType.Pattern)
			{
				return Rounds.Select(round => round.GetResolver(new SelectedTargetPattern(TargetingRule, round.Pattern ?? TargetingRule.Pattern, target.Team, target.Anchor)));
			}
			else return Rounds.Select(round => round.GetResolver(target));
		}

		public SkillTargetingSet(ITargetingRule targetingRule)
		{
			TargetingRule = targetingRule;
		}

		public class Builder : BuilderBase<SkillTargetingSet, Builder>
		{
			private readonly ITargetingRule targetingRule;
			private List<SkillOperationDefinition> rounds;

			public Builder(ITargetingRule targetingRule)
			{
				this.targetingRule = targetingRule;
				Reset();
			}

			public override Builder Reset()
			{
				rounds = new List<SkillOperationDefinition>();
				build = new SkillTargetingSet(targetingRule)
				{
					Rounds = rounds.AsReadOnly()
				};
				return this;
			}

			public Builder AddCombatRound(float executionStart, OperationProvider getOperation)
				=> AddCombatRound(new SkillOperationDefinition(executionStart, getOperation));

			public Builder AddCombatRound(float executionStart, ITargetPattern pattern, OperationProvider getOperation)
				=> AddCombatRound(new SkillOperationDefinition(executionStart, pattern, getOperation));

			public Builder AddCombatRound(SkillOperationDefinition round)
			{
				rounds.Add(round);
				return this;
			}
		}
	}
}
