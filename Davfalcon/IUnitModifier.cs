namespace Davfalcon
{
	/// <summary>
	/// Exposes properties of unit modifiers.
	/// </summary>
	public interface IUnitModifier : IUnit, IDescribable
	{
		/// <summary>
		/// Gets the name of the modifier.
		/// </summary>
		new string Name { get; }

		/// <summary>
		/// Gets the unit the object is modifying.
		/// </summary>
		IUnit Target { get; }

		/// <summary>
		/// Binds the modifier to a new target.
		/// </summary>
		/// <param name="target">The new object to bind to.</param>
		void Bind(IUnit target);
	}
}
