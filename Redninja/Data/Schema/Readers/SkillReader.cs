using System.Collections.Generic;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema.Readers
{
	internal static class SkillReader
	{
		public static void ReadRoot(SkillRootSchema skillRoot, IEditableDataManager manager)
		{
			ReadTargets(manager.SkillTargetingRules, skillRoot.TargetingRules);
			ReadSkills(manager.Skills, manager.SkillTargetingRules, skillRoot.CombatSkills);
		}

		internal static void ReadSkills(IEditableDataStore<ISkill> skillStore, IDataStore<ITargetingRule> targetStore, List<CombatSkillSchema> skills)
		{
			foreach (CombatSkillSchema item in skills)
			{
				skillStore[item.DataId] = CombatSkill.Build(item.Name, item.DefaultParameters, b =>
				{
					b.SetActionTime(ParseHelper.ParseActionTime(item.Time));
					item.TargetingSets.ForEach(ts => b.AddTargetingSet(targetStore[ts.TargetingRuleId],
						set =>
						{
							foreach (CombatRoundSchema combatRound in ts.CombatRounds)
							{
								set.AddCombatRound(
									combatRound.ExecutionStart,
									combatRound.Pattern != null ? ParseHelper.ParsePattern(combatRound.Pattern) : null,
									ParseHelper.ParseOperationProvider(combatRound.OperationProviderName),
									combatRound.Parameters ?? item.DefaultParameters);
							}
							return set;
						}));
					return b;
				});
			}
		}

		internal static void ReadTargets(IEditableDataStore<ITargetingRule> targetStore, List<TargetingRuleSchema> targets)
		{
			foreach (TargetingRuleSchema item in targets)
			{
				TargetingRule rule;
				if (item.TargetType == TargetType.Pattern)
				{
					ITargetPattern pattern = ParseHelper.ParsePattern(item.Pattern);
					rule = new TargetingRule(pattern, item.TargetTeam, ParseHelper.ParseTargetCondition(item.TargetConditionName));
				}
				else
				{
					rule = new TargetingRule(item.TargetTeam, ParseHelper.ParseTargetCondition(item.TargetConditionName));
				}

				targetStore[item.DataId] = rule;
			}
		}
	}
}
