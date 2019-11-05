using Redninja.Components.Combat;

namespace Redninja.Components.Buffs
{
	public interface IBuffExecutionBehavior : IOperationSource
	{
		void Initialize(IBuff buff);
		void OnClockTick(float delta, IBuff buff);
	}
}
