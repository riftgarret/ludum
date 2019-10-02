namespace Davfalcon.Collections.Generic
{
	/// <summary>
	/// Exposes methods to register and retrieve prototypes.
	/// </summary>
	/// <typeparam name="T">The type of prototypes in the registry.</typeparam>
	public interface IRegistry<T>
	{
		/// <summary>
		/// Add a new prototype to the registry under a specified name.
		/// </summary>
		/// <param name="prototype">The prototype to be added to the registry.</param>
		/// <param name="name">The name to be associated with <paramref name="prototype"/>.</param>
		void Register(T prototype, string name);

		/// <summary>
		/// Gets a copy of the prototype by name.
		/// </summary>
		/// <param name="name">The name the prototype was registered with.</param>
		/// <returns>A copy of the prototype, if one was registered with <paramref name="name"/>; otherwise, <c>null</c>.</returns>
		T Get(string name);
	}
}
