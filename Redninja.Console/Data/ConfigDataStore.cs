using Redninja.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.ConsoleDriver.Data
{
	public class ConfigDataStore<T> : IDataStore<T>
	{
		private readonly Dictionary<string, T> dict = new Dictionary<string, T>();

		public T this[string key]
		{
			get => dict[key];
			set => dict[key] = value;
		}
	}
}
