using System;
using Davfalcon.Revelator;

namespace Redninja.BattleSystem
{
	public interface IBattleEntity : IClock
	{
		IUnit Character { get; }
		IBattleAction Action { get; set; }
		PhaseState Phase { get; }
		float PhasePercent { get; }
		bool IsPlayerControlled { get; set; }
		EntityPosition Position { get; }
		int Team { get; set; }

		event Action<IBattleEntity> DecisionRequired;

		void InitializeBattlePhase();
		void MovePosition(int row, int col);
	}
}