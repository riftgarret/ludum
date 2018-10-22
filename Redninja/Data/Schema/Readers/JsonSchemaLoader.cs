using System;

namespace Redninja.Data.Schema.Readers
{
	internal class JsonSchemaLoader
	{
		private readonly string configPath;

		public JsonSchemaLoader(string configPath)
		{
			if (String.IsNullOrWhiteSpace(configPath))
			{
				throw new ArgumentException("Config path must be specified.", nameof(configPath));
			}

			this.configPath = configPath;
		}

		public void Read(IEditableDataManager manager)
		{
			// These will need to be loaded in the correct order for dependencies
			ConfigSchema config = ParseHelper.ReadJson<ConfigSchema>(configPath);
			SkillReader.ReadRoot(ParseHelper.ReadJson<SkillRootSchema>(config.SkillsPath), manager);
			ClassReader.ReadRoot(ParseHelper.ReadJson<ClassesRootSchema>(config.ClassesPath), manager.Classes, manager.Skills);
			AIRuleReader.ReadRoot(ParseHelper.ReadJson<AIRulesRootSchema>(config.AIRulesPath), manager);
			AIBehaviorReader.ReadRoot(ParseHelper.ReadJson<AIRuleSetRootSchema>(config.AIBehaviorsPath), manager);
			CharacterReader.ReadRoot(ParseHelper.ReadJson<CharactersRootSchema>(config.CharactersPath), manager);
			EncounterReader.ReadRoot(ParseHelper.ReadJson<EncountersRootSchema>(config.EncountersPath), manager);
		}
	}
}
