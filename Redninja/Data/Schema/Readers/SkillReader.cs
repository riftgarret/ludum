using System;
using System.Collections.Generic;
using System.Linq;
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
				skillStore[item.DataId] = CombatSkill.Build(b => b
					.SetName(item.Name)
					.SetActionTime(ParseHelper.ParseActionTime(item.Time))
					.SetDamage(item.BaseDamage)
					.SetBonusDamageStat(item.BonusDamageStat)
					.AddDamageTypes(item.DamageTypes.Select(t => (Enum)t))
					.AddTargetingSets(item.TargetingSets.Select(ts =>
						SkillTargetingSet.Build(targetStore[ts.TargetingRuleId], builder =>
						{
							foreach (CombatRoundSchema combatRound in ts.CombatRounds)
							{
								if (combatRound.Pattern != null)
								{
									builder.AddCombatRound(
										combatRound.ExecutionStart,
										ParseHelper.ParsePattern(combatRound.Pattern),
										ParseHelper.ParseOperationProvider(combatRound.OperationProviderName));
								}
								else
								{
									builder.AddCombatRound(
										combatRound.ExecutionStart,
										ParseHelper.ParseOperationProvider(combatRound.OperationProviderName));
								}
							}
							return builder;
						}))));
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
