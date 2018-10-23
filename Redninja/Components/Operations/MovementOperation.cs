using Redninja.Components.Combat;

namespace Redninja.Components.Operations
{
	internal class MovementOperation : BattleOperationBase
	{
		private readonly IUnitModel unit;
		private readonly int row;
		private readonly int col;

		public MovementOperation(IUnitModel unit, int row, int col)
		{
			this.unit = unit;
			this.row = row;
			this.col = col;
		}

		public override void Execute(IBattleModel manager, ICombatExecutor combatExecutor)
		{
			combatExecutor.MoveEntity(unit, row, col);
		}
	}
}
