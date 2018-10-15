namespace Redninja.Components.Operations
{
	public abstract class BattleOperationBase : IBattleOperation
	{
		public abstract void Execute(IBattleEntityManager entityManager, ICombatExecutor combatExecutor);
	}
}
