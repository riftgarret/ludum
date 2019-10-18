using System.Collections.Generic;
using Redninja.Components.Decisions.AI;

namespace Redninja.Data.Schema.Readers
{
	internal static class AIBehaviorReader
	{			
		public static void ReadRoot(List<AIRuleSetSchema> ruleSets, IEditableDataManager manager)
		{
			foreach(AIRuleSetSchema rs in ruleSets)
			{
				AIRuleSet.Builder b = new AIRuleSet.Builder();
				foreach(string ruleId in rs.RuleIds)
				{
					IAIRule rule = manager.AIRules[ruleId];
					b.AddRule(rule);
				}

				manager.AIBehavior[rs.DataId] = b.Build();
			}
		}
	}
}
