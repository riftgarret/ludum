namespace Davfalcon.Builders
{
	/// <summary>
	/// Exposes a method to build the specified type of objects.
	/// </summary>
	/// <typeparam name="T">The type of objects to build.</typeparam>
	public interface IBuilder<T>
	{
		/// <summary>
		/// Gets the built object.
		/// </summary>
		/// <returns>The built object.</returns>
		T Build();
	}
}
