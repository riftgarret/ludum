using System;

namespace Davfalcon
{
	/// <summary>
	/// Represents an object that modifies an entity's stats.
	/// </summary>
	/// <typeparam name="T">The type of entity that the modifier affects.</typeparam>
	public interface IStatsModifier<T> : IModifier<T> where T : class
	{
		/// <summary>
		/// Gets a set of modifications that will affect the entity's stats.
		/// </summary>
		IStats GetStatModifications(Enum type);
	}
}
