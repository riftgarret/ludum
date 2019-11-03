namespace Redninja.Components.Combat
{
	internal abstract class BattleOperationBase : IBattleOperation
	{
		protected BattleOperationBase(float executionStart) => ExecutionStart = executionStart;

		public float ExecutionStart { get; protected set; }
		public bool Executed { get; private set; } = false;

		public void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor)
		{
			OnExecute(battleModel, combatExecutor);
			Executed = true;
		}

		protected abstract void OnExecute(IBattleModel battleModel, ICombatExecutor combatExecutor);
	}
}
