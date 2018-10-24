using System.Collections.Generic;
using System.Linq;
using Davfalcon.Builders;
using Redninja.Components.Targeting;
using ParamsFunc = Redninja.Components.Skills.SkillOperationParameters.Builder.Func;

namespace Redninja.Components.Skills
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

		internal SkillTargetingSet(ITargetingRule targetingRule)
		{
			TargetingRule = targetingRule;
		}

		public class Builder : BuilderBase<SkillTargetingSet, Builder>
		{
			private readonly ITargetingRule targetingRule;
			private readonly ISkillOperationParameters defaultArgs;
			private List<SkillOperationDefinition> rounds;

			internal Builder(ITargetingRule targetingRule, ISkillOperationParameters defaultArgs)
			{
				this.targetingRule = targetingRule;
				this.defaultArgs = defaultArgs;
				Reset();
			}

			public override Builder Reset()
			{
				rounds = new List<SkillOperationDefinition>();
				SkillTargetingSet s = new SkillTargetingSet(targetingRule)
				{
					Rounds = rounds.AsReadOnly()
				};
				return Reset(s);
			}

			public Builder AddCombatRound(SkillOperationDefinition round) => Self(s => rounds.Add(round));

			// Schema loading
			public Builder AddCombatRound(float executionStart, OperationProvider getOperation, ISkillOperationParameters args)
				=> AddCombatRound(new SkillOperationDefinition(executionStart, getOperation, args));

			public Builder AddCombatRound(float executionStart, ITargetPattern pattern, OperationProvider getOperation, ISkillOperationParameters args)
				=> AddCombatRound(new SkillOperationDefinition(executionStart, pattern, getOperation, args));

			// Functional creation
			public Builder AddCombatRound(float executionStart, OperationProvider getOperation)
				=> AddCombatRound(executionStart, getOperation, defaultArgs);

			public Builder AddCombatRound(float executionStart, ITargetPattern pattern, OperationProvider getOperation)
				=> AddCombatRound(executionStart, pattern, getOperation, defaultArgs);

			public Builder AddCombatRound(float executionStart, OperationProvider getOperation, ParamsFunc args)
				=> AddCombatRound(executionStart, getOperation, args(new SkillOperationParameters.Builder(defaultArgs.Name)).Build());

			public Builder AddCombatRound(float executionStart, ITargetPattern pattern, OperationProvider getOperation, ParamsFunc args)
				=> AddCombatRound(executionStart, pattern, getOperation, args(new SkillOperationParameters.Builder(defaultArgs.Name)).Build());
		}
	}
}
