﻿using System.Collections.Generic;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema.Readers
{
	internal static class SkillReader
	{		
		public static void ReadAll(SkillRootSchema skillRoot, IEditableDataManager manager)
		{					
			ReadTargets(manager.SkillTargetSets, skillRoot.TargetSets);
			ReadSkills(manager.Skills, manager.SkillTargetSets, skillRoot.CombatSkills);
		}

		public static void ReadSkills(IEditableDataStore<ISkill> skillStore, IEditableDataStore<SkillTargetingSet> targetStore, List<CombatSkillSchema> skills)
		{
			foreach (var item in skills)
			{
				CombatSkill.Builder builder = new CombatSkill.Builder();
				builder.SetName(item.Name);
				builder.SetActionTime(ParseHelper.ParseActionTime(item.Time));
				builder.SetDamage(item.BaseDamage, item.BonusDamageStat);
				
				item.DamageTypes.ForEach(dt => builder.AddDamageType(dt));
				item.TargetSetIds.ForEach(tsId => builder.AddTargetingSet(targetStore[tsId]));

				skillStore[item.DataId] = builder.Build();
			}
		}

		public static void ReadTargets(IEditableDataStore<SkillTargetingSet> targetStore, List<TargetingSetSchema> targets)
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
					builder.AddCombatRound(
						combatRound.ExecutionStart,
						ParseHelper.ParsePattern(combatRound.Pattern),
						ParseHelper.ParseOperationProvider(combatRound.OperationProviderName));
				}

				targetStore[item.DataId] = builder.Build();
			}
		}
	}
}
