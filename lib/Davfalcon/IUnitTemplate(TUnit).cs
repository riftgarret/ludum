using System;

namespace Davfalcon
{
	/// <summary>
	/// Exposes basic properties of a unit.
	/// </summary>
	/// <typeparam name="TUnit">The interface used by the unit's implementation.</typeparam>
	public interface IUnitTemplate<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		/// <summary>
		/// Gets the unit's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the unit's stats.
		/// </summary>
		IStatsProperties Stats { get; }

		/// <summary>
		/// Gets the modifiers attached to the unit.
		/// </summary>
		IModifierStack<TUnit> Modifiers { get; }

		/// <summary>
		/// Gets the specified unit component.
		/// </summary>
		/// <typeparam name="TComponent">The type of the returned component.</typeparam>
		/// <param name="id">The enum identifier for the component.</param>
		/// <returns>The specified component.</returns>
		TComponent GetComponent<TComponent>(Enum id) where TComponent : class, IUnitComponent<TUnit>;
	}
}
