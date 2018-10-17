using System;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Decisions;

namespace Redninja.Entities
{
	public interface IBattleEntity : IUnitModel, IClockSynchronized
	{
		bool IsPlayerControlled { get; }
		IBattleAction CurrentAction { get; }
		IActionDecider ActionDecider { get; }

		event Action<IBattleEntity, IBattleAction> ActionSet;
		event Action<IBattleEntity> ActionNeeded;

		void InitializeBattlePhase();
		void MovePosition(int row, int col);
		void SetAction(IBattleAction action);
	}
}
