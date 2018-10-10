namespace Redninja.Targeting
{
	public interface ITargetingRule
	{
		ITargetPattern Pattern { get; }
		TargetType Type { get; }

		bool IsValidTarget(IBattleEntity entity);
	}
}