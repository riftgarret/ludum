namespace Redninja.Components.Combat
{
	internal abstract class BattleOperationBase : IBattleOperation
	{
		public abstract void Execute(IBattleModel battleModel, ICombatExecutor combatExecutor);
	}
}
