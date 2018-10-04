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
		float TimeAction { get; }
		float TimeRecover { get; }
	}
}

