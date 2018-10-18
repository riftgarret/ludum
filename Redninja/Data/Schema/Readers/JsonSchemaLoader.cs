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
			throw new System.ArgumentException("Config path must be specified.", nameof(configPath));
		 }

		 this.configPath = configPath;
	  }

	  public void Read(IEditableDataManager manager)
	  {
		 ConfigSchema config = ParseHelper.ReadJson<ConfigSchema>(configPath);
		 SkillReader.ReadRoot(ParseHelper.ReadJson<SkillRootSchema>(config.SkillsPath), manager);
		 AIRuleReader.ReadRoot(ParseHelper.ReadJson<AIRulesRootSchema>(config.AIRulesPath), manager);
		 AIBehaviorReader.ReadRoot(ParseHelper.ReadJson<AIRuleSetRootSchema>(config.AIBehaviorsPath), manager);		 
		 CharacterReader.ReadRoot(ParseHelper.ReadJson<CharactersRootSchema>(config.CharactersPath), manager);
		 EncounterReader.ReadRoot(ParseHelper.ReadJson<EncountersRootSchema>(config.EncountersPath), manager);
	  }
   }
}
