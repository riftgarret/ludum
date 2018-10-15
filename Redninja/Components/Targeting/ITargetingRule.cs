namespace Redninja.Components.Targeting
{
	public interface ITargetingRule
	{
		TargetType Type { get; }
		TargetTeam Team { get; }
		ITargetPattern Pattern { get; }

		bool IsValidTarget(IEntityModel target, IEntityModel user);
	}
}
