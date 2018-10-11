using Davfalcon.Revelator;
using Redninja.Decisions;
using Redninja.Skills;
using Redninja.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.AI
{
	public class AIRuleSet
	{
		// TODO add a default rule for standard attack to all rules, talk to rice about best course
		private List<IAIRule> Rules { get; } = new List<IAIRule>();		

		public IBattleAction ResolveAction(IBattleEntity source, IBattleEntityManager bem) 
		{
			// TODO filter by refresh time so we dont repick the same skill by refresh requirement
			// find rules triggers
			IEnumerable<IAIRule> validRules = Rules.Where(rule => rule.IsValidTriggerConditions(source, bem));				

			// assign pool
			WeightedPool<IAIRule> weightedPool = new WeightedPool<IAIRule>();
			validRules.ToList().ForEach(x => weightedPool.Add(x, x.Weight));			
			
			// cycle through rules until we find one we can assign
			while(weightedPool.Count() > 0)
			{
				// pick weighted rule
				IAIRule rule = weightedPool.Random();

				IBattleAction action = rule.GenerateAction(source, bem);
				
				if(action != null)
				{
					return action;
				}

				// no targets found for any skill, prune and research
				weightedPool.Remove(rule);
			}

			throw new InvalidProgramException("We couldnt find any rules to use, we should have implemented attack for all!");
		}


		public class Builder : IBuilder<AIRuleSet>
		{
			private AIRuleSet ruleSet;

			public Builder() => Reset();

			public Builder Reset()
			{
				ruleSet = new AIRuleSet();
				return this;
			}

			public void AddRule(IAIRule rule) => ruleSet.Rules.Add(rule);

			public AIRuleSet Build()
			{
				return new AIRuleSet();
			}
		}
	}
}
