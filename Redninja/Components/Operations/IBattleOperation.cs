using Redninja.Components.Combat;

namespace Redninja.Components.Operations
{
	public interface IBattleOperation
	{
		void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor);
	}
}
