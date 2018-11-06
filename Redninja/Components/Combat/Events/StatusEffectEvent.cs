using System;
using Davfalcon.Revelator;

namespace Redninja.Components.Combat.Events
{
	public class StatusEffectEvent : ICombatEvent
	{
		public IUnitModel Source { get; }
		public IUnitModel Target { get; }
		public IBuff StatusEffect { get; }

		public StatusEffectEvent(IUnitModel source, IUnitModel target, IBuff statusEffect)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Target = target ?? throw new ArgumentNullException(nameof(target));
			StatusEffect = statusEffect ?? throw new ArgumentNullException(nameof(statusEffect));
		}
	}
}
