using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema.Readers
{
	internal class AIRuleItemFactory : IDataItemFactory<IAIRule>
	{
		public IAIRule CreateInstance(string dataId, ISchemaStore store)
		{
			var rs = store.GetSchema<AISkillRuleSchema>(dataId);

			AISkillRule.Builder b = new AISkillRule.Builder();
			b.SetName(rs.DataId);
			b.SetRefreshTime(rs.RefreshTime);
			b.SetRuleTargetType(rs.TargetType);
			b.SetWeight(rs.Weight);

			rs.TriggerConditions.ForEach(x => b.AddTriggerCondition(ParseHelper.ParseCondition(x)));
			rs.TargetConditions.ForEach(x => b.AddFilterCondition(ParseHelper.ParseCondition(x)));
			
			foreach (var skillPriorityItem in rs.SkillPriorityMap)
			{
				ISkill skill = store.SingleInstance<ISkill>(skillPriorityItem.Key);
				IAITargetPriority priority = ParseHelper.ParseAITargetPriority(skillPriorityItem.Value);
				b.AddSkillAndPriority(skill, priority);
			}

			return b.Build();
		}
	}
}
