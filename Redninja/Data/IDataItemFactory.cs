using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data
{
	public interface IDataItemFactory<TYPE> where TYPE : class
	{
		TYPE CreateInstance(string dataId, ISchemaStore store);
	}
}
