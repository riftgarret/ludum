using System;
using Davfalcon;
using Redninja.Components.Clock;
using Redninja.Components.Operations;

namespace Redninja.Components.Actions
{
	public interface IBattleAction : INameable, IClockSynchronized
    {
		ActionTime Time { get; }
        ActionPhase Phase { get; }
		float PhaseTime { get; }
        float PhaseProgress { get; }

		event Action<IBattleAction> ActionExecuting;
		event Action<float, IBattleOperation> BattleOperationReady;

		void Start();
	}
}


