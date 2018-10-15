using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.Data;

namespace Redninja.ConsoleDriver.Data
{
	class DataManager : IDataManager
	{		
		private ConfigDataStore<ISkill> skills = new ConfigDataStore<ISkill>();
		public IDataStore<ISkill> Skills => skills;

		public ConfigDataStore<IAIRule> aiRules = new ConfigDataStore<IAIRule>();
		public IDataStore<IAIRule> AIRules => aiRules;

		public ConfigDataStore<AIRuleSet> aiBehavior = new ConfigDataStore<AIRuleSet>();
		public IDataStore<AIRuleSet> AIBehavior => aiBehavior;

		public ConfigDataStore<IAITargetCondition> aiTargetCondition = new ConfigDataStore<IAITargetCondition>();
		public IDataStore<IAITargetCondition> AITargetCondition => aiTargetCondition;

		public ConfigDataStore<IAITargetPriority> aiTargetPriority = new ConfigDataStore<IAITargetPriority>();
		public IDataStore<IAITargetPriority> AITargetPriority => aiTargetPriority;

		public ConfigDataStore<IUnit> npcUnits = new ConfigDataStore<IUnit>();
		public IDataStore<IUnit> NPCUnits => npcUnits;

		public ConfigDataStore<SkillTargetingSet> skillTargetSets = new ConfigDataStore<SkillTargetingSet>();
		public IDataStore<SkillTargetingSet> SkillTargetSets => skillTargetSets;

		public void Initialize()
		{
			LoadInstances();
		}

		private void LoadInstances()
		{
			SkillReader reader = new SkillReader();
			reader.ReadAll(skills, skillTargetSets);
		}

		

		private void BindInstances()
		{

		}
	}
}
