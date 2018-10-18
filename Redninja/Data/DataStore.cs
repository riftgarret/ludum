using System;
using System.Collections.Generic;

namespace Redninja.Data
{
	internal class DataStore<T> : IEditableDataStore<T>
	{
		private readonly Dictionary<string, T> dict = new Dictionary<string, T>();

		public T this[string key]
		{
			get
			{
				if (!dict.ContainsKey(key)) throw new InvalidOperationException($"Missing key detected: '{key}' in dataStore:{typeof(T)}");
				return dict[key];
			}
			set
			{
				if (dict.ContainsKey(key)) throw new InvalidOperationException($"Duplicate key detected: '{key}' in dataStore:{typeof(T)}");
				dict[key] = value;
			}
		}
	}
}
