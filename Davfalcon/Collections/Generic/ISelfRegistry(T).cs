namespace Davfalcon.Collections.Generic
{
	/// <summary>
	/// Exposes methods to register <see cref="INameable"/> prototypes.
	/// </summary>
	/// <typeparam name="T">The type of prototypes in the registry.</typeparam>
	public interface ISelfRegistry<T> : IRegistry<T> where T : INameable
	{
		/// <summary>
		/// Add a new prototype to the registry. The prototype will be registered with its <see cref="INameable.Name"/> property as the name.
		/// </summary>
		/// <param name="prototype">The prototype to be added to the registry.</param>
		void Register(T prototype);
	}
}
