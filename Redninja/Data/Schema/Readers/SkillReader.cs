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
			ReadTargets(manager.SkillTargetSets, skillRoot.TargetSets);
			ReadSkills(manager.Skills, manager.SkillTargetSets, skillRoot.CombatSkills);
		}

		internal static void ReadSkills(IEditableDataStore<ISkill> skillStore, IEditableDataStore<SkillTargetingSet> targetStore, List<CombatSkillSchema> skills)
		{
			foreach (var item in skills)
			{
				skillStore[item.DataId] = CombatSkill.Build(b => b
					.SetName(item.Name)
					.SetActionTime(ParseHelper.ParseActionTime(item.Time))
					.SetDamage(item.BaseDamage)
					.SetBonusDamageStat(item.BonusDamageStat)
					.AddDamageTypes(item.DamageTypes.Select(t => (Enum)t))
					.AddTargetingSets(item.TargetSetIds.Select(tsId => targetStore[tsId])));
			}
		}

		internal static void ReadTargets(IEditableDataStore<SkillTargetingSet> targetStore, List<TargetingSetSchema> targets)
		{
			foreach (var item in targets)
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

				var builder = new SkillTargetingSet.Builder(rule);
				foreach (var combatRound in item.CombatRounds)
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

				targetStore[item.DataId] = builder.Build();
			}
		}
	}
}
