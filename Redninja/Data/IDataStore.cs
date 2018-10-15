using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data
{
	public interface IDataStore<T>
	{
		T this[string key] { get; }
	}
}
