using Davfalcon.Revelator;
using Redninja.Components.Clock;
using Redninja.Components.Combat;

namespace Redninja.Components.Skills.StatusEffects
{
	public interface IStatusEffect : IBuff, IOperationSource, IClockSynchronized
	{
		float TimeDuration { get; }
		float TimeInterval { get; }
		float RemainingTime { get; }
	}
}
