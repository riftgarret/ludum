using System;

namespace Davfalcon
{
	/// <summary>
	/// Generic abstract base class for modifiers.
	/// </summary>
	/// <typeparam name="T">The type of entity that the modifier affects.</typeparam>
	[Serializable]
	public abstract class Modifier<T> : IModifier<T> where T : class
	{
		[NonSerialized]
		private Func<T> getTarget;

		/// <summary>
		/// Gets or sets the modifier's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the modifier's description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets the entity being modified.
		/// </summary>
		public T Target => getTarget?.Invoke();

		/// <summary>
		/// Binds the modifier to a new entity.
		/// </summary>
		/// <param name="target">The new entity to bind to.</param>
		public virtual void Bind(T target)
		{
			//this.target = target;
			getTarget = () => target;
		}

		/// <summary>
		/// Sets the modifier to defer target lookup.
		/// </summary>
		/// <param name="func">A function that returns the entity the modifier should affect.</param>
		public virtual void Bind(Func<T> func)
		{
			getTarget = func;
		}

		/// <summary>
		/// Returns a representation of the modified object.
		/// </summary>
		/// <returns>A representation of the modified object.</returns>
		public abstract T AsModified();
	}
}
