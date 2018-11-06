namespace Redninja.Components.Combat
{
	public interface IBattleOperation
	{
		void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor);
	}
}
