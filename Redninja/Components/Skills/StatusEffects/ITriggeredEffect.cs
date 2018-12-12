using System.Collections.Generic;

namespace Redninja.Components.Skills.StatusEffects
{
	public interface ITriggeredEffect : IConditionalEffect
	{
		IEnumerable<IEventTrigger> Triggers { get; }
		float Cooldown { get; }
	}
}
