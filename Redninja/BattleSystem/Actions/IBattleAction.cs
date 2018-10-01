using System;
using Redninja.BattleSystem.Combat.Operation;
using Redninja.BattleSystem.Entity;

namespace Redninja.BattleSystem.Actions
{
    public interface IBattleAction : IGameClock
    {
		event Action<BattleEntity, ICombatOperation> ActionExecuted;

		float TimePrepare { get; }
        float TimeAction { get; }
        float TimeRecover { get; }

		// Consider representing these as a struct to reduce property clutter
        float PhaseClock { get; }
        float PhaseComplete { get; }
        float PhasePercent { get; }
        PhaseState Phase { get; }
    }
}


