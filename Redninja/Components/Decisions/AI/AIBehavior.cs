using System;
using System.Collections.Generic;
using Davfalcon.Builders;

namespace Redninja.Components.Decisions.AI
{
	public class AIBehavior
	{		
		public List<IAIRule> Rules { get; } = new List<IAIRule>();				

		public class Builder : IBuilder<AIBehavior>
		{
			private AIBehavior ruleSet;

			public Builder() => Reset();

			public Builder Reset()
			{
				ruleSet = new AIBehavior();
				return this;
			}

			private Builder Self(Action<Builder> action)
			{
				action(this);
				return this;
			}

			public void AddRule(IAIRule rule) => Self(x => ruleSet.Rules.Add(rule));

			public AIBehavior Build()
			{
				AIBehavior builtSet = ruleSet;				
				Reset();
				return builtSet;
			}
		}
	}
}
