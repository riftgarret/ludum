using Redninja.Components.Combat;

namespace Redninja.Components.Buffs
{
	public interface IBuffExecutionBehavior : IOperationSource
	{
		void OnClockTick(float delta, IBuff buff);
	}
}
