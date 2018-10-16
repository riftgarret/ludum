namespace Redninja.Data
{
	public interface IEditableDataStore<T> : IDataStore<T>
	{
		new T this[string key] { get; set; }
	}
}
