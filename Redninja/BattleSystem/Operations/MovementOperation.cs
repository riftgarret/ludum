using System;
using Redninja.BattleSystem.Events;

namespace Redninja.BattleSystem.Operations
{
	public class MovementOperation : IBattleOperation, IMovementEvent
	{
		private readonly IBattleEntity unit;
		private readonly int row;
		private readonly int col;

		public IBattleEntity Entity { get; }
		public EntityPosition NewPosition { get; }
		public EntityPosition OriginalPosition { get; }

		public float ExecutionStartTime { get; }

		public event Action<IBattleEvent> BattleEventOccurred;

		public void Execute(IBattleEntityManager manager, ICombatExecutor combatResolver)
		{
			unit.MovePosition(row, col);
		}

		public MovementOperation(IBattleEntity unit, int row, int col)
		{
			this.unit = unit;
			this.row = row;
			this.col = col;
		}
	}
}
