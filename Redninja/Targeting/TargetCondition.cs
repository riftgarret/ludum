namespace Redninja.Targeting
{
	public delegate bool TargetCondition(IBattleEntity entity);

	public static class TargetConditions
	{
		public static TargetCondition MustBeAlive { get; } = e => e.Character.VolatileStats[CombatStats.HP] > 0;
		public static TargetCondition MustBeDead { get; } = e => e.Character.VolatileStats[CombatStats.HP] <= 0;
		public static TargetCondition MustBeOnTeam(int teamId) => e => e.Team == teamId;
	}
}
