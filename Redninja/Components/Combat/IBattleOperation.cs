namespace Redninja.Components.Combat
{
	public interface IBattleOperation
	{
		bool Executed { get; }
		float ExecutionStart { get; }
		void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor);
	}
}
