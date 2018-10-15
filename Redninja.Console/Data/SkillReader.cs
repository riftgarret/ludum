using Newtonsoft.Json;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.ConsoleDriver.Data.Schema;
using System.Collections.Generic;
using System.IO;

namespace Redninja.ConsoleDriver.Data
{
	/**
	 * TODO: Put schema definition in here or on wiki?
	 */
	internal class SkillReader
	{
		private const string SKILLS_FILE = "Assets/Data/skills.json";

		internal void ReadAll(ConfigDataStore<ISkill> skillStore, ConfigDataStore<SkillTargetingSet> targetStore)
		{			

			var json = File.ReadAllText(SKILLS_FILE);
			var skillRoot = JsonConvert.DeserializeObject<SkillRootSchema>(json);

			ReadTargets(targetStore, skillRoot.TargetSets);
			ReadSkills(skillStore, targetStore, skillRoot.CombatSkills);
		}

		internal void ReadSkills(ConfigDataStore<ISkill> skillStore, ConfigDataStore<SkillTargetingSet> targetStore, List<CombatSkillSchema> skills)
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

		internal void ReadTargets(ConfigDataStore<SkillTargetingSet> targetStore, List<TargetingSetSchema> targets)
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
