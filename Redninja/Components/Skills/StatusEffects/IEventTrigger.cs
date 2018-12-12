using Redninja.Components.Combat.Events;

namespace Redninja.Components.Skills.StatusEffects
{
	public interface IEventTrigger
	{
		void IsValid(ICombatEvent battleEvent, IUnitModel self);
	}
}
