using System.Collections.Generic;
using Davfalcon.Serialization;

namespace Davfalcon.Collections.Generic
{
	/// <summary>
	/// Register prototypes to be cloned.
	/// </summary>
	/// <typeparam name="T">The type of prototypes in the registry.</typeparam>
	public class PrototypeCloner<T> : IRegistry<T>
	{
		private Dictionary<string, T> lookup = new Dictionary<string, T>();

		/// <summary>
		/// Add a new prototype to the registry under a specified name.
		/// </summary>
		/// <param name="prototype">The prototype to be added to the registry.</param>
		/// <param name="name">The name to be associated with <paramref name="prototype"/>.</param>
		public void Register(T prototype, string name)
			=> lookup.Add(name, prototype);

		/// <summary>
		/// Gets a deep clone of the prototype by name.
		/// </summary>
		/// <param name="name">The name the prototype was registered with.</param>
		/// <returns>A deep clone of the prototype, if one was registered with <paramref name="name"/>; otherwise, <c>null</c>.</returns>
		public T Get(string name)
			=> (T)Serializer.DeepClone(lookup[name]);
	}
}
