using System;
using Redninja.Events;

namespace Redninja.Operations
{
	public class MovementOperation : IBattleOperation
	{
		private readonly IBattleEntity unit;
		private readonly int row;
		private readonly int col;

		public float ExecutionStartTime { get; }

		public void Execute(IBattleEntityManager manager, ICombatExecutor combatExecutor)
		{
			combatExecutor.MoveEntity(unit, row, col);
		}

		public MovementOperation(IBattleEntity unit, int row, int col)
		{
			this.unit = unit;
			this.row = row;
			this.col = col;
		}
	}
}
