namespace Davfalcon
{
	public interface IStatsHolder
	{
		/// <summary>
		/// Gets the unit's stats.
		/// </summary>
		IStats Stats { get; }

		/// <summary>
		/// Gets a breakdown of the unit's stats.
		/// </summary>
		IStatsPackage StatsDetails { get; }
	}
}
