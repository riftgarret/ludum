namespace Redninja.Targeting
{
	public interface ITargetingRule
	{
		TargetType Type { get; }
		TargetTeam Team { get; }
		ITargetPattern Pattern { get; }

		bool IsValidTarget(IBattleEntity target, IBattleEntity user);
	}
}