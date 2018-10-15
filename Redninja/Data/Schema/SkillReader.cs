using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using System.Collections.Generic;

namespace Redninja.Data.Schema
{
	internal static class SkillReader
	{		
		internal static void ReadAll(SkillRootSchema skillRoot, IEditableDataManager manager)
		{					
			ReadTargets(manager.SkillTargetSets, skillRoot.TargetSets);
			ReadSkills(manager.Skills, manager.SkillTargetSets, skillRoot.CombatSkills);
		}

		internal static void ReadSkills(IEditableDataStore<ISkill> skillStore, IEditableDataStore<SkillTargetingSet> targetStore, List<CombatSkillSchema> skills)
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

		internal static void ReadTargets(IEditableDataStore<SkillTargetingSet> targetStore, List<TargetingSetSchema> targets)
		{
			foreach (var item in targets)
			{
				// note: Other constructor not visibile, should figure out how to determine whcih to use.
				var rule = new TargetingRule(item.TargetTeam, ParseHelper.ParseTargetCondition(item.TargetConditionEnum));

				var builder = new SkillTargetingSet.Builder(rule);
				foreach (var combatRound in item.CombatRounds)
				{
					builder.AddCombatRound(
						combatRound.ExecutionStart,
						ParseHelper.ParsePattern(combatRound.Pattern),
						ParseHelper.ParseOperation(combatRound.OperationEnum));
				}

				targetStore[item.DataId] = builder.Build();
			}
		}
	}
}
