using System.Collections.Generic;

namespace Redninja.Data
{
	internal class DataStore<T> : IEditableDataStore<T>
	{
		private readonly Dictionary<string, T> dict = new Dictionary<string, T>();

		public T this[string key]
		{
			get => dict[key];
			set => dict[key] = value;
		}
	}
}
