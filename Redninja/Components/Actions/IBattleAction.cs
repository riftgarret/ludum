using System;
using Davfalcon;
using Redninja.Components.Clock;
using Redninja.Components.Combat;

namespace Redninja.Components.Actions
{
	public interface IBattleAction : IClockSynchronized, IOperationSource
	{
		string Name { get; }
		ActionTime Time { get; }
        ActionPhase Phase { get; }
		float PhaseTime { get; }
        float PhaseProgress { get; }

		event Action<IBattleAction> ActionExecuting;

		void Start();
	}
}


