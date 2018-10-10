namespace Redninja.Targeting
{
	public delegate bool TargetCondition(IBattleEntity target, IBattleEntity user);

	public static class TargetConditions
	{
		public static TargetCondition MustBeAlive { get; } = (t, u) => t.Character.VolatileStats[CombatStats.HP] > 0;
		public static TargetCondition MustBeDead { get; } = (t, u) => t.Character.VolatileStats[CombatStats.HP] <= 0;
		public static TargetCondition MustBeSameTeam => (t, u) => t.Team == u.Team;
		public static TargetCondition MustBeDifferentTeam => (t, u) => t.Team != u.Team;
	}
}
