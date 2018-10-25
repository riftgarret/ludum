using System;

namespace Redninja.Components.Combat.Events
{
	public class StatusEffectEvent : IBattleEvent
	{
		public IUnitModel Entity { get; }
		public IStatusEffect StatusEffect { get; }

		public StatusEffectEvent(IUnitModel entity, IStatusEffect statusEffect)
		{
			Entity = entity ?? throw new ArgumentNullException(nameof(entity));
			StatusEffect = statusEffect ?? throw new ArgumentNullException(nameof(statusEffect));
		}
	}
}
