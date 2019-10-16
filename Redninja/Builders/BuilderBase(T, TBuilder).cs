namespace Redninja
	/// Base class for implementing extendable builders.
	/// </summary>
	/// <typeparam name="T">The type of the object to be constructed.</typeparam>
	/// <typeparam name="TBuilder">The type of the implemented builder.</typeparam>
	public abstract class BuilderBase<T, TBuilder> : BuilderBase<T, T, TBuilder> where TBuilder : BuilderBase<T, T, TBuilder>
	{
	}
}
