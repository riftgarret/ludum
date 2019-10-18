using System;
using System.Collections.Generic;

namespace Redninja.Components.Decisions.AI
{
	public class AIRuleSet
	{		
		public List<IAIRule> Rules { get; } = new List<IAIRule>();				

		public class Builder : IBuilder<AIRuleSet>
		{
			private AIRuleSet ruleSet;

			public Builder() => Reset();

			public Builder Reset()
			{
				ruleSet = new AIRuleSet();
				return this;
			}

			private Builder Self(Action<Builder> action)
			{
				action(this);
				return this;
			}

			public void AddRule(IAIRule rule) => Self(x => ruleSet.Rules.Add(rule));

			public AIRuleSet Build()
			{
				AIRuleSet builtSet = ruleSet;				
				Reset();
				return builtSet;
			}
		}
	}
}
