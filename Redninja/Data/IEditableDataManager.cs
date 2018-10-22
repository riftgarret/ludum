using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.System;

namespace Redninja.Data
{
	/// <summary>
	/// Data structures that should deserialized into this data store to be 
	/// pulled together later when needed.
	/// </summary>
	public interface IEditableDataManager
	{
		IEditableDataStore<ISkill> Skills { get; }

		IEditableDataStore<IAIRule> AIRules { get; }

		IEditableDataStore<AIRuleSet> AIBehavior { get; }

		IEditableDataStore<IAITargetCondition> AITargetCondition { get; }

		IEditableDataStore<IAITargetPriority> AITargetPriority { get; }

		IEditableDataStore<IUnit> NPCUnits { get; }

		IEditableDataStore<SkillTargetingSet> SkillTargetSets { get; }

		IEditableDataStore<Encounter> Encounters { get; }

		IEditableDataStore<IClassProvider> Classes { get; }

		IEditableDataStore<T> GetDataStore<T>();		
	}
}


