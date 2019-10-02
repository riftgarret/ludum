namespace Davfalcon
{
	/// <summary>
	/// Represents an object that has an editable description.
	/// </summary>
	public interface IEditableDescription : IEditableName
	{
		/// <summary>
		/// Gets or sets the description of the object.
		/// </summary>
		string Description { get; set; }
	}
}
