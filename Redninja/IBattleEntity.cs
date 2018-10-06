using System;
using Davfalcon.Revelator;

namespace Redninja
{
	public interface IBattleEntity : IClockSynchronized
	{
		IUnit Character { get; }
		int Team { get; set; }
		bool IsPlayerControlled { get; }
		EntityPosition Position { get; }
		IBattleAction CurrentAction { get; }
		PhaseState Phase { get; }
		float PhasePercent { get; }
		IActionDecider ActionDecider { get; set; }

		event Action<IBattleEntity> DecisionRequired;

		void InitializeBattlePhase();
		void SetAction(IBattleAction action);
		void MovePosition(int row, int col);
	}
}