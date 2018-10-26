using System;
using Davfalcon.Revelator;

namespace Redninja.Components.Combat.Events
{
	public class StatusEffectEvent : IBattleEvent
	{
		public IUnitModel Entity { get; }
		public IBuff StatusEffect { get; }

		public StatusEffectEvent(IUnitModel entity, IBuff statusEffect)
		{
			Entity = entity ?? throw new ArgumentNullException(nameof(entity));
			StatusEffect = statusEffect ?? throw new ArgumentNullException(nameof(statusEffect));
		}
	}
}
