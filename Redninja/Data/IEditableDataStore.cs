using Redninja.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data
{
	public interface IEditableDataStore<T> : IDataStore<T>
	{
		new T this[string key] { get;  set; }		
	}
}
