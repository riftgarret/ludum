using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Decisions.AI;

namespace Redninja.Data.Schema.Readers
{
	internal class AIBehaviorItemFactory : IDataItemFactory<AIRuleSet>
	{
		public AIRuleSet CreateInstance(string dataId, ISchemaStore store)
		{
			AIRuleSetSchema rs = store.GetSchema<AIRuleSetSchema>(dataId);

			AIRuleSet.Builder b = new AIRuleSet.Builder();
			foreach (string ruleid in rs.RuleIds)
			{
				b.AddRule(store.SingleInstance<IAIRule>(ruleid));
			}

			return b.Build();
		}
	}
}
