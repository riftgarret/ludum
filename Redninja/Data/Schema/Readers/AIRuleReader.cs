using System.Collections.Generic;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema.Readers
{
	internal static class AIRuleReader
	{
		public static void ReadRoot(AIRulesRootSchema rulesRoot, IEditableDataManager manager)
		{
			ReadSkillRules(rulesRoot.SkillRules, manager);
			ReadAttackRules(rulesRoot.AttackRules, manager);
		}

		internal static void ReadSkillRules(List<AISkillRuleSchema> rules, IEditableDataManager manager)
		{
			foreach (AISkillRuleSchema rs in rules)
			{
				AISkillRule.Builder b = new AISkillRule.Builder();
				b.SetName(rs.DataId);
				b.SetRefreshTime(rs.RefreshTime);
				b.SetRuleTargetType(rs.TargetType);
				b.SetWeight(rs.Weight);

				ReadTriggerConditions(b, rs.TriggerConditions);				

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

		internal static void ReadAttackRules(List<AIAttackRuleSchema> rules, IEditableDataManager manager)
		{
			foreach (AIAttackRuleSchema rs in rules)
			{
				AIAttackRule.Builder b = new AIAttackRule.Builder();
				b.SetName(rs.DataId);
				b.SetRefreshTime(rs.RefreshTime);
				b.SetTargetPriority(ParseHelper.ParseAITargetPriority(rs.Priority));
				b.SetWeight(rs.Weight);

				ReadTriggerConditions(b, rs.TriggerConditions);

				manager.AIRules[rs.DataId] = b.Build();
			}
		}		

		/// <summary>
		/// Read trigger conditions.
		/// </summary>
		/// <typeparam name="ParentBuilder"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="b"></param>
		/// <param name="triggerConditions"></param>
		private static void ReadTriggerConditions<ParentBuilder, T>(
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
