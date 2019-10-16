using System;
using Davfalcon.Buffs;

namespace Redninja.Components.Buffs
{
	public interface IBuff : IBuff<IUnit>
	{
		IUnit Owner { get; set; }

		// copied from IStatusEffect
		float TimeDuration { get; }

		float TimeRemaining { get; }

		float EffectInterval { get; }

		// need to define effect payload
		event Action<IBuff> Effect;
		
		// add method to hook up buff to battle
	}
}
