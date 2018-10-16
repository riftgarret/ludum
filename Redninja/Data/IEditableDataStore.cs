namespace Redninja.Data
{
	internal interface IEditableDataStore<T> : IDataStore<T>
	{
		new T this[string key] { get; set; }
	}
}
