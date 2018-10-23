using Davfalcon.Revelator;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.System;

namespace Redninja.Data
{
	/// <summary>
	/// Data structures that should deserialized into this data store to be 
	/// pulled together later when needed.
	/// </summary>
	public interface IDataManager
	{
		IDataStore<ISkill> Skills { get; }

		IDataStore<IAIRule> AIRules { get; }

		IDataStore<AIBehavior> AIBehavior { get; }

		IDataStore<IAITargetCondition> AITargetCondition { get; }

		IDataStore<IAITargetPriority> AITargetPriority { get; }

		IDataStore<IUnit> NPCUnits { get; }

		IDataStore<ITargetingRule> SkillTargetingRules { get; }

		IDataStore<Encounter> Encounters { get; }

		IDataStore<IClassProvider> Classes { get; }
	}
}


