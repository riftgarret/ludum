namespace Davfalcon.Collections.Generic
{
	/// <summary>
	/// Register <see cref="INameable"/> prototypes to be cloned.
	/// </summary>
	/// <typeparam name="T">The type of prototypes in the registry.</typeparam>
	public class SelfRegisteredPrototypeCloner<T> : PrototypeCloner<T>, ISelfRegistry<T> where T : INameable
	{
		/// <summary>
		/// Add a new prototype to the registry. The prototype will be registered with its <see cref="INameable.Name"/> property as the name.
		/// </summary>
		/// <param name="prototype">The prototype to be added to the registry.</param>
		public void Register(T prototype) => Register(prototype, prototype.Name);
	}
}
