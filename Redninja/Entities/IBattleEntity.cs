using System;
using Redninja.Components.Actions;
using Redninja.Components.Clock;

namespace Redninja.Entities
{
	public interface IBattleEntity : IUnitModel, IClockSynchronized
	{
		bool IsPlayerControlled { get; }
		IBattleAction CurrentAction { get; }
		IActionDecider ActionDecider { get; set; }

		event Action<IBattleEntity> DecisionRequired;

		void InitializeBattlePhase();
		void SetAction(IBattleAction action);
	}
}
