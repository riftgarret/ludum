namespace Redninja.Operations
{
	public abstract class BattleOperationBase : IBattleOperation
	{
		public float ExecutionStartTime { get; }

		public BattleOperationBase(float time)
			=> ExecutionStartTime = time;

		public abstract void Execute(IBattleEntityManager manager, ICombatExecutor combatExecutor);
	}
}
