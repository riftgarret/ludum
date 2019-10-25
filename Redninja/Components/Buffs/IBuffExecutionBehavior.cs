using Redninja.Components.Combat;

namespace Redninja.Components.Buffs
{
	public interface IBuffExecutionBehavior : IOperationSource
	{
		// this should probably take IBuff instead
		void OnClockTick(float delta, ActiveBuff activeBuff);
	}
}
