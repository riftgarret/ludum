using System.Collections.Generic;

namespace Davfalcon
{
	/// <summary>
	/// Represents a stack of modifiers affecting a single object.
	/// </summary>
	/// <typeparam name="T">The type of object that the modifiers affect.</typeparam>
	public interface IModifierStack<T> : IModifier<T>, IList<IModifier<T>> where T : class
	{
		// Inherited members only
	}
}
