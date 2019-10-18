using System;
using System.Collections.Generic;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Data.Schema.Readers;
using Redninja.System;
using IBuff = Redninja.Components.Buffs.IBuff;

namespace Redninja.Data
{
	internal class DataManager : IDataManager, IEditableDataManager
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

		public IDataStore<IBuff> Buffs => GetDataStore<IBuff>();
		public IDataStore<ISkill> Skills => GetDataStore<ISkill>();
		public IDataStore<IAIRule> AIRules => GetDataStore<IAIRule>();
		public IDataStore<AIRuleSet> AIBehavior => GetDataStore<AIRuleSet>();
		public IDataStore<IAITargetCondition> AITargetCondition => GetDataStore<IAITargetCondition>();
		public IDataStore<IAITargetPriority> AITargetPriority => GetDataStore<IAITargetPriority>();
		public IDataStore<IUnit> NPCUnits => GetDataStore<IUnit>();
		public IDataStore<ITargetingRule> SkillTargetingRules => GetDataStore<ITargetingRule>();
		public IDataStore<Encounter> Encounters => GetDataStore<Encounter>();
		public IDataStore<IClassProvider> Classes => GetDataStore<IClassProvider>();

		IEditableDataStore<IBuff> IEditableDataManager.Buffs => GetDataStore<IBuff>();
		IEditableDataStore<ISkill> IEditableDataManager.Skills => GetDataStore<ISkill>();
		IEditableDataStore<IAIRule> IEditableDataManager.AIRules => GetDataStore<IAIRule>();
		IEditableDataStore<AIRuleSet> IEditableDataManager.AIBehavior => GetDataStore<AIRuleSet>();
		IEditableDataStore<IAITargetCondition> IEditableDataManager.AITargetCondition => GetDataStore<IAITargetCondition>();
		IEditableDataStore<IAITargetPriority> IEditableDataManager.AITargetPriority => GetDataStore<IAITargetPriority>();
		IEditableDataStore<IUnit> IEditableDataManager.NPCUnits => GetDataStore<IUnit>();
		IEditableDataStore<ITargetingRule> IEditableDataManager.SkillTargetingRules => GetDataStore<ITargetingRule>();
		IEditableDataStore<Encounter> IEditableDataManager.Encounters => GetDataStore<Encounter>();
		IEditableDataStore<IClassProvider> IEditableDataManager.Classes => GetDataStore<IClassProvider>();

		IEditableDataStore<T> IEditableDataManager.GetDataStore<T>() => GetDataStore<T>();

		public void LoadJson(string configPath) => new JsonSchemaLoader(configPath).Read(this);
		public void Load(IDataLoader reader) => reader.Load(this);
	}
}
