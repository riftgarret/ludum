using System;
using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.Data.Schema.Readers;

namespace Redninja.Data
{
	public class DataManager : IDataManager, IEditableDataManager
	{
		private readonly Dictionary<Type, object> dataTypeLookup = new Dictionary<Type, object>();

		private DataStore<T> GetDataStore<T>()
		{
			Type type = typeof(T);
			if (!dataTypeLookup.ContainsKey(type))
			{
				dataTypeLookup[type] = new DataStore<T>();
			}
			return dataTypeLookup[type] as DataStore<T>;
		}

		public IDataStore<ISkill> Skills => GetDataStore<ISkill>();
		public IDataStore<IAIRule> AIRules => GetDataStore<IAIRule>();
		public IDataStore<AIBehavior> AIBehavior => GetDataStore<AIBehavior>();
		public IDataStore<IAITargetCondition> AITargetCondition => GetDataStore<IAITargetCondition>();
		public IDataStore<IAITargetPriority> AITargetPriority => GetDataStore<IAITargetPriority>();
		public IDataStore<IUnit> NPCUnits => GetDataStore<IUnit>();
		public IDataStore<SkillTargetingSet> SkillTargetSets => GetDataStore<SkillTargetingSet>();
		public IDataStore<Encounter> Encounters => GetDataStore<Encounter>();

		IEditableDataStore<ISkill> IEditableDataManager.Skills => GetDataStore<ISkill>();
		IEditableDataStore<IAIRule> IEditableDataManager.AIRules => GetDataStore<IAIRule>();
		IEditableDataStore<AIBehavior> IEditableDataManager.AIBehavior => GetDataStore<AIBehavior>();
		IEditableDataStore<IAITargetCondition> IEditableDataManager.AITargetCondition => GetDataStore<IAITargetCondition>();
		IEditableDataStore<IAITargetPriority> IEditableDataManager.AITargetPriority => GetDataStore<IAITargetPriority>();
		IEditableDataStore<IUnit> IEditableDataManager.NPCUnits => GetDataStore<IUnit>();
		IEditableDataStore<SkillTargetingSet> IEditableDataManager.SkillTargetSets => GetDataStore<SkillTargetingSet>();
		IEditableDataStore<Encounter> IEditableDataManager.Encounters => GetDataStore<Encounter>();

		IEditableDataStore<T> IEditableDataManager.GetDataStore<T>() => GetDataStore<T>();

		public void LoadJson(string configPath) => new JsonSchemaLoader(configPath).Read(this);
		public void Load(IDataLoader reader) => reader.Load(this);
	}
}
