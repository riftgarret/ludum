using Redninja.Components.Combat;

namespace Redninja.Components.Actions
{
	public class MovementAction : BattleActionBase
	{
		private readonly IBattleEntity entity;
		private readonly int row;
		private readonly int col;

		protected override void ExecuteAction(float timeDelta, float time)
		{
			if (PhaseProgress >= 1)
				SendBattleOperation(GetPhaseTimeAt(0), new MovementOperation(entity, row, col));
		}

		public MovementAction(IBattleEntity entity, int row, int col)
			: base("Movement", 3, 5, 3)
		{
			this.entity = entity;
			this.row = row;
			this.col = col;
		}
	}
}
