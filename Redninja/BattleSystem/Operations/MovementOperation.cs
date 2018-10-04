using System;
using Redninja.BattleSystem.Events;

namespace Redninja.BattleSystem.Operations
{
	public class MovementOperation : IBattleOperation
	{
		private readonly IBattleEntity unit;
		private readonly int row;
		private readonly int col;

		public float ExecutionStartTime { get; }

		public event Action<IBattleEvent> BattleEventOccurred;

		public void Execute(IBattleEntityManager manager, ICombatExecutor combatResolver)
		{
			EntityPosition position = unit.Position;
			unit.MovePosition(row, col);
			BattleEventOccurred?.Invoke(new MovementEvent(unit, unit.Position, position));
		}

		public MovementOperation(IBattleEntity unit, int row, int col)
		{
			this.unit = unit;
			this.row = row;
			this.col = col;
		}
	}
}
