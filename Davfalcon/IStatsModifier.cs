namespace Davfalcon
{
	/// <summary>
	/// Represents an object that modifies a unit's stats.
	/// </summary>
	public interface IStatsModifier : IUnitModifier
	{
		/// <summary>
		/// Gets the values that this item will add to the unit's stats.
		/// </summary>
		IStats Additions { get; }

		/// <summary>
		/// Gets the multipliers that this item will apply to the unit's stats.
		/// </summary>
		IStats Multipliers { get; }
	}
}
