namespace Redninja.Operations
{
	public abstract class BattleOperationBase : IBattleOperation
	{
		public abstract void Execute(IBattleEntityManager manager, ICombatExecutor combatExecutor);
	}
}
