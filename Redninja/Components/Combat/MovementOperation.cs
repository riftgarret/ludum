namespace Redninja.Components.Combat
{
	internal class MovementOperation : BattleOperationBase
	{
		private readonly IBattleEntity unit;
		private readonly int row;
		private readonly int col;

		public MovementOperation(IBattleEntity unit, int row, int col) : base(1)
		{
			this.unit = unit;
			this.row = row;
			this.col = col;
		}

		protected override void OnExecute(IBattleModel manager, ICombatExecutor combatExecutor)
		{
			combatExecutor.MoveEntity(unit, row, col);
		}
	}
}
