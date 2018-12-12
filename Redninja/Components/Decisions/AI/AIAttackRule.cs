using System;

namespace Redninja.Components.Decisions.AI
{
	internal class AIAttackRule : AIRuleBase, IAIAttackRule
	{
		public IAITargetPriority TargetPriority { get; private set; }

		public class Builder : BuilderBase<Builder, AIAttackRule>
		{
			private AIAttackRule rule;			

			public Builder() => Reset();

			public override AIAttackRule Build()
			{
				if (rule.TargetPriority == null) throw new InvalidOperationException($"Missing IAITargetProperity");
				BuildBase();
				AIAttackRule builtRule = this.rule;
				Reset();
				return builtRule;
			}

			public Builder SetTargetPriority(IAITargetPriority priority)
			{
				rule.TargetPriority = priority;
				return this;
			}

			public Builder Reset()
			{
				rule = new AIAttackRule();				
				ResetBase(rule);
				return this;
			}
		}
	}
}
