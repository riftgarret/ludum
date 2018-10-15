namespace Redninja.Components.Operations
{
	public interface IBattleOperation
	{
		void Execute(IBattleEntityManager manager, ICombatExecutor combatExecutor);
	}
}
