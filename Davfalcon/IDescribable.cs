namespace Davfalcon
{
	/// <summary>
	/// Represents an object that has a description.
	/// </summary>
	public interface IDescribable : INameable
	{
		/// <summary>
		/// Gets the description of the object.
		/// </summary>
		string Description { get; }
	}
}
