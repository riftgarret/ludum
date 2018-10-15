namespace Redninja.Components.Actions.Decisions.AI
{
	public interface IAITargetCondition
	{
		bool IsValid(IBattleEntity entity);
	}
}
