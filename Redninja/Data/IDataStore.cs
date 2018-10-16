namespace Redninja.Data
{
	public interface IDataStore<T>
	{
		T this[string key] { get; }
	}
}
