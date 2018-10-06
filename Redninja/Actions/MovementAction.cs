using Redninja.Operations;

namespace Redninja.Actions
{
	public class MovementAction : BattleActionBase
	{
		private readonly IBattleEntity entity;
		private readonly int row;
		private readonly int col;

		protected override void ExecuteAction(float timeDelta, float time)
		{
			if (PhaseProgress >= 1)
				SendBattleOperation(new MovementOperation(entity, row, col));
		}

		public MovementAction(IBattleEntity entity, int row, int col)
			: base(3, 5, 3)
		{
			this.entity = entity;
			this.row = row;
			this.col = col;
		}
	}
}
