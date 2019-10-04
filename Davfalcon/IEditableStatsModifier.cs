namespace Davfalcon
{
	/// <summary>
	/// Exposes properties to edit a modifier's stats.
	/// </summary>
	public interface IEditableStatsModifier : IUnitModifier
	{
		/// <summary>
		/// Gets the values that this item will add to the unit's stats in an editable format.
		/// </summary>
		IEditableStats Additions { get; }

		/// <summary>
		/// Gets the multipliers that this item will apply to the unit's stats in an editable format.
		/// </summary>
		IEditableStats Multipliers { get; }
	}
}
