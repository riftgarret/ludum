using System;

namespace Redninja.BattleSystem
{
	public interface IBattleAction : IClockSynchronized
    {
		event Action<IBattleAction> ActionExecuting;
		event Action<IBattleOperation> BattleOperationReady;

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


