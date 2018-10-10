namespace Redninja.Targeting
{
	public interface ITargetingRule
	{
		int MaxTargets { get; }
		ITargetPattern Pattern { get; }
		TargetType Type { get; }

		bool IsValidTarget(IBattleEntity entity);
	}
}