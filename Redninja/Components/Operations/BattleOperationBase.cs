using Redninja.Components.Combat;

namespace Redninja.Components.Operations
{
	internal abstract class BattleOperationBase : IBattleOperation
	{
		public abstract void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor);
	}
}
