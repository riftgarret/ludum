using System;

namespace Davfalcon
{
	/// <summary>
	/// Represents an object that modifies an entity.
	/// </summary>
	/// <typeparam name="T">The type of entity that the modifier affects.</typeparam>
	public interface IModifier<T> where T : class
	{
		/// <summary>
		/// Gets the modifier's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets a description of the modifier.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Gets the entity being modified.
		/// </summary>
		T Target { get; }

		/// <summary>
		/// Binds the modifier to a new entity.
		/// </summary>
		/// <param name="target">The new entity to bind to.</param>
		void Bind(T target);

		/// <summary>
		/// Sets the modifier to defer target lookup.
		/// </summary>
		/// <param name="func">A function that returns the entity the modifier should affect.</param>
		void Bind(Func<T> targetFunc);

		/// <summary>
		/// Gets a representation of the modified entity.
		/// </summary>
		/// <returns>A representation of the modified entity.</returns>
		T AsModified();
	}
}
