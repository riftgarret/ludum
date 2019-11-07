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

			ReadTriggerConditions(b, rs.TriggerConditions);

			foreach (string targetConditionExp in rs.TargetConditions)
			{
				IAITargetCondition cond = ParseHelper.ParseAITargetCondition(targetConditionExp);
				b.AddFilterCondition(cond);
			}

			foreach (var skillPriorityItem in rs.SkillPriorityMap)
			{
				ISkill skill = store.SingleInstance<ISkill>(skillPriorityItem.Key);
				IAITargetPriority priority = ParseHelper.ParseAITargetPriority(skillPriorityItem.Value);
				b.AddSkillAndPriority(skill, priority);
			}

			return b.Build();
		}

		private void ReadTriggerConditions<ParentBuilder, T>(
			AIRuleBase.BuilderBase<ParentBuilder, T> b,
			Dictionary<TargetTeam, List<string>> triggerConditions)
			where ParentBuilder : AIRuleBase.BuilderBase<ParentBuilder, T>
			where T : AIRuleBase
		{
			foreach (var item in triggerConditions)
			{
				foreach (string condName in item.Value)
				{
					IAITargetCondition cond = ParseHelper.ParseAITargetCondition(condName);
					b.AddTriggerCondition(item.Key, cond);
				}
			}
		}
	}
}
