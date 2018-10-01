using System;
using Davfalcon.Revelator;
using Redninja.BattleSystem.Actions;

namespace Redninja.BattleSystem.Entity
{
	public interface IBattleEntity
	{
		IUnit Character { get; }
		IBattleAction Action { get; set; }
		PhaseState Phase { get; }
		float PhasePercent { get; }
		bool IsPlayerControlled { get; set; }
		EntityPosition Position { get; }
		int Team { get; set; }

		event Action<IBattleEntity> DecisionRequired;

		void IncrementGameClock(float gameClockDelta);
		void InitializeBattlePhase();
		void MovePosition(int row, int col);
	}
}