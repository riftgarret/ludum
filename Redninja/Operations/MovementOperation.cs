namespace Redninja.Operations
{
	public class MovementOperation : BattleOperationBase
	{
		private readonly IBattleEntity unit;
		private readonly int row;
		private readonly int col;

		public MovementOperation(float time, IBattleEntity unit, int row, int col)
			: base(time)
		{
			this.unit = unit;
			this.row = row;
			this.col = col;
		}

		public override void Execute(IBattleEntityManager manager, ICombatExecutor combatExecutor)
		{
			combatExecutor.MoveEntity(unit, row, col);
		}
	}
}
