using System;
using Davfalcon.Revelator;
using Redninja.Components.Clock;
using Redninja.Components.Combat;

namespace Redninja.Components.Skills.StatusEffects
{
	public interface IStatusEffect : IBuff, IOperationSource, IClockSynchronized
	{
		float TimeDuration { get; }
		float TimeRemaining { get; }
		float EffectInterval { get; }

		IUnitModel EffectTarget { get; set; }

		event Action<IStatusEffect> Expired;
	}
}
