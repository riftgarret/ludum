namespace Davfalcon
{
	/// <summary>
	/// Exposes basic properties of a unit.
	/// </summary>
	public interface IUnit : INameable, IStatsHolder
	{
		/// <summary>
		/// Gets the unit's name.
		/// </summary>
		new string Name { get; }
		
		/// <summary>
		/// Gets the unit's class.
		/// </summary>
		string Class { get; }

		/// <summary>
		/// Gets the unit's level.
		/// </summary>
		int Level { get; }

		/// <summary>
		/// Gets the modifiers attached to the unit.
		/// </summary>
		IUnitModifierStack Modifiers { get; }
	}
}
