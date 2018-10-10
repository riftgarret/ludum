using System;

namespace Redninja
{
	public interface IBattleAction : IClockSynchronized
    {
		event Action<IBattleAction> ActionExecuting;
		event Action<float, IBattleOperation> BattleOperationReady;

        PhaseState Phase { get; }
		float PhaseTime { get; }
        float PhaseProgress { get; }

		float TimePrepare { get; }
		float TimeExecute { get; }
		float TimeRecover { get; }

		void Start();
		void Start(IClock clock);
	}
}


