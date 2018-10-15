using Redninja.Components.Combat;

namespace Redninja.Components.Operations
{
	public class MovementOperation : BattleOperationBase
	{
		private readonly IEntityModel unit;
		private readonly int row;
		private readonly int col;

		public MovementOperation(IEntityModel unit, int row, int col)
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
