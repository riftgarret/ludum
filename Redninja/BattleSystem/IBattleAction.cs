using System;

namespace Redninja.BattleSystem
{
	public interface IBattleAction : IClock
    {
		event Action<IBattleAction> ActionExecuting;
		event Action<IBattleOperation> BattleOperationReady;

		float TimePrepare { get; }
        float TimeAction { get; }
        float TimeRecover { get; }

        float PhaseComplete { get; }
        float PhasePercent { get; }
        PhaseState Phase { get; }
    }
}


