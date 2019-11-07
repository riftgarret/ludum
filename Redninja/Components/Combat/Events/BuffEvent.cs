using System;
using Redninja.Components.Buffs;

namespace Redninja.Components.Combat.Events
{
	public class BuffEvent : ICombatEvent
	{
		public IBattleEntity Source { get; }
		public IBattleEntity Target { get; }
		public IBuff StatusEffect { get; }

		public BuffEvent(IBattleEntity source, IBattleEntity target, IBuff statusEffect)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Target = target ?? throw new ArgumentNullException(nameof(target));
			StatusEffect = statusEffect ?? throw new ArgumentNullException(nameof(statusEffect));
		}
	}
}
