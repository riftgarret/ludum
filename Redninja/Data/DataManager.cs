using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.Data;
using Redninja.Data.Schema;

namespace Redninja.ConsoleDriver.Data
{
	public class DataManager : IDataManager
	{		
		private DataStore<ISkill> skills = new DataStore<ISkill>();
		public IDataStore<ISkill> Skills => skills;

		public DataStore<IAIRule> aiRules = new DataStore<IAIRule>();
		public IDataStore<IAIRule> AIRules => aiRules;

		public DataStore<AIRuleSet> aiBehavior = new DataStore<AIRuleSet>();
		public IDataStore<AIRuleSet> AIBehavior => aiBehavior;

		public DataStore<IAITargetCondition> aiTargetCondition = new DataStore<IAITargetCondition>();
		public IDataStore<IAITargetCondition> AITargetCondition => aiTargetCondition;

		public DataStore<IAITargetPriority> aiTargetPriority = new DataStore<IAITargetPriority>();
		public IDataStore<IAITargetPriority> AITargetPriority => aiTargetPriority;

		public DataStore<IUnit> npcUnits = new DataStore<IUnit>();
		public IDataStore<IUnit> NPCUnits => npcUnits;

		public DataStore<SkillTargetingSet> skillTargetSets = new DataStore<SkillTargetingSet>();
		public IDataStore<SkillTargetingSet> SkillTargetSets => skillTargetSets;

		public void Initialize(string configPath)
		{			
			DataReader.Read(AsWritable(), configPath);
		}		

		private IEditableDataManager AsWritable()
		{
			return new EditableManager(this);
		}

		private class EditableManager : IEditableDataManager
		{
			private DataManager inner;

			internal EditableManager(DataManager innerManager)
			{
				this.inner = innerManager;
			}

			public IEditableDataStore<ISkill> Skills => inner.skills;

			public IEditableDataStore<IAIRule> AIRules => inner.aiRules;

			public IEditableDataStore<AIRuleSet> AIBehavior => inner.aiBehavior;

			public IEditableDataStore<IAITargetCondition> AITargetCondition => inner.aiTargetCondition;

			public IEditableDataStore<IAITargetPriority> AITargetPriority => inner.aiTargetPriority;

			public IEditableDataStore<IUnit> NPCUnits => inner.npcUnits;

			public IEditableDataStore<SkillTargetingSet> SkillTargetSets => inner.skillTargetSets;
		}
	}
}
