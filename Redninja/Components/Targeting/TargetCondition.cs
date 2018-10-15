namespace Redninja.Components.Targeting
{
	public delegate bool TargetCondition(IUnitModel target, IUnitModel user);

	public static class TargetConditions
	{
		public static TargetCondition None { get; } = (t, u) => true;
		public static TargetCondition MustBeAlive { get; } = (t, u) => t.Character.VolatileStats[CombatStats.HP] > 0;
		public static TargetCondition MustBeDead { get; } = (t, u) => t.Character.VolatileStats[CombatStats.HP] <= 0;
		public static TargetCondition MustBeSameTeam { get; } = (t, u) => t.Team == u.Team;
		public static TargetCondition MustBeDifferentTeam { get; } = (t, u) => t.Team != u.Team;
	}
}
