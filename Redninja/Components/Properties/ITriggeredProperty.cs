using System;
using System.Collections.Generic;
using Redninja.Components.Buffs;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Properties
{
	/// <summary>
	/// Triggered property.
	/// TODO the goal of having these properties exposed is so we can, on
	/// the UI side, build up a tooltip by looking at these properties.
	/// </summary>
	public interface ITriggeredProperty : IItemProperty
	{
		TriggerPropertyType TriggerType { get; }

		IEnumerable<ITriggerCondition> Conditions { get; }

		int RefreshTime { get; }

		IBuff Buff { get; }

		ISkill Skill { get; }

		IEnumerable<ITriggerCondition> TargetFilter { get; }
	}
}
