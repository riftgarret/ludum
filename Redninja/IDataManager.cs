using Davfalcon.Revelator;
using Redninja.AI;
using Redninja.Data;
using Redninja.Skills;
using System;

namespace Redninja
{
	/// <summary>
	/// Data structures that should deserialized into this data store to be 
	/// pulled together later when needed.
	/// </summary>
	public interface IDataManager
	{
		IDataStore<ISkill> Skills { get; }

		IDataStore<IAIRule> AIRules { get; }

		IDataStore<AIRuleSet> AIBehavior { get; }

		IDataStore<IAITargetCondition> AITargetCondition { get; }

		IDataStore<IAITargetPriority> AITargetPriority { get; }

		IDataStore<IUnit> NPCUnits { get; }
	}
}


