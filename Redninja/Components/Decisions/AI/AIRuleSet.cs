using System.Collections.Generic;
using System.Linq;
using Davfalcon.Builders;
using Redninja.Components.Actions;
using Redninja.Logging;

namespace Redninja.Components.Decisions.AI
{
	public class AIRuleSet
	{
		// TODO add a default rule for standard attack to all rules, talk to rice about best course
		private List<IAIRule> Rules { get; } = new List<IAIRule>();		

		/// <summary>
		/// Resolve an action from these AI Rules. If no action can be found, it will return null.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="bem"></param>
		/// <param name="historyState"></param>
		/// <returns></returns>
		public IBattleAction ResolveAction(IBattleEntity source, IDecisionHelper decisionHelper, IAIHistoryState historyState) 
		{
			// DEBUG rule name -> in pool
			var debugRuleMeta = new Dictionary<string, bool>();
			Rules.ForEach(x => debugRuleMeta[x.RuleName] = false);			

			// find rules triggers
			IEnumerable<IAIRule> validRules = Rules.Where(rule => rule.IsValidTriggerConditions(source, decisionHelper));				

			// assign pool
			WeightedPool<IAIRule> weightedPool = new WeightedPool<IAIRule>();
			foreach(var rule in validRules.Where(x => historyState.IsRuleReady(x)))
			{
				weightedPool.Add(rule, rule.Weight);
				debugRuleMeta[rule.RuleName] = true;
			}
			
			// cycle through rules until we find one we can assign
			while(weightedPool.Count() > 0)
			{
				// pick weighted rule
				IAIRule rule = weightedPool.Random();

				IBattleAction action = rule.GenerateAction(source, decisionHelper);
				
				if(action != null)
				{
					LogResult(debugRuleMeta, rule, action);
					historyState.AddEntry(rule, action);
					return action;
				}

				// no targets found for any skill, prune and research
				weightedPool.Remove(rule);
			}
			LogResult(debugRuleMeta, null, null);
			return null;
		}

		private void LogResult(Dictionary<string, bool> ruleMeta, IAIRule resolvedRule, IBattleAction resultAction)
		{
			string ruleStr = resolvedRule != null ? resolvedRule.RuleName : "NONE";
			string actionStr = resultAction != null ? resultAction.GetType().ToString() : "NONE";
			RLog.D(this, $"RuleSet - resultRule:{ruleStr} resultAction: {actionStr}\n\t{ruleMeta}");
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
				AIRuleSet builtSet = ruleSet;
				Reset();
				return builtSet;
			}
		}
	}
}
