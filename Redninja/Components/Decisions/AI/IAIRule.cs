using System;
using System.Collections.Generic;
using Redninja.Components.Conditions;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions.AI
{
	public interface IAIRule
	{
		int RefreshTime { get; }
		string RuleName { get; }
		int Weight { get; }

		IEnumerable<ICondition> TriggerConditions { get; }
	}
}
