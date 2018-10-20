using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;

namespace Redninja.Data.Schema.Readers
{
	internal static class AIBehaviorReader
	{			
		public static void ReadRoot(List<AIRuleSetSchema> ruleSets, IEditableDataManager manager)
		{
			foreach(AIRuleSetSchema rs in ruleSets)
			{
				AIBehavior.Builder b = new AIBehavior.Builder();
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
