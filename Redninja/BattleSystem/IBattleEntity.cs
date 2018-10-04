using System;
using Davfalcon.Revelator;

namespace Redninja.BattleSystem
{
	public interface IBattleEntity : IClockSynchronized
	{
		IUnit Character { get; }
		int Team { get; set; }
		bool IsPlayerControlled { get; }
		EntityPosition Position { get; }
		IBattleAction Action { get; set; }
		PhaseState Phase { get; }
		float PhasePercent { get; }
		IActionDecider ActionDecider { get; set; }

		event Action<IBattleEntity> DecisionRequired;

		void InitializeBattlePhase();
		void MovePosition(int row, int col);
	}
}