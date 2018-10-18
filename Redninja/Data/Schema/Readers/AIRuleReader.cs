using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;

namespace Redninja.Data.Schema.Readers
{
	internal static class AIRuleReader
	{
		public static void ReadRoot(AIRulesRootSchema rulesRoot, IEditableDataManager manager)
		{
			ReadRules(rulesRoot.SkillRules, manager);
		}

		internal static void ReadRules(List<AISkillRuleSchema> rules, IEditableDataManager manager)
		{
			foreach (AISkillRuleSchema rs in rules)
			{
				AISkillRule.Builder b = new AISkillRule.Builder();
				b.SetName(rs.RuleName);
				b.SetRefreshTime(rs.RefreshTime);
				b.SetRuleTargetType(rs.TargetType);
				b.SetWeight(rs.Weight);

				
				foreach (var item in rs.TriggerConditions)
				{
					foreach (string condName in item.Value)
					{
						IAITargetCondition cond = ParseHelper.ParseAITargetCondition(condName);
						b.AddTriggerCondition(item.Key, cond);
					}
				}

				foreach (string filterConditionName in rs.FilterConditions)
				{
					IAITargetCondition cond = ParseHelper.ParseAITargetCondition(filterConditionName);
					b.AddFilterCondition(cond);
				}

				foreach(var skillPriorityItem in rs.SkillPriorityMap)
				{
					ISkill skill = manager.Skills[skillPriorityItem.Key];
					IAITargetPriority priority = ParseHelper.ParseAITargetPriority(skillPriorityItem.Value);
					b.AddSkillAndPriority(skill, priority);
				}

				manager.AIRules[rs.DataId] = b.Build();
			}
		}
	}
}
