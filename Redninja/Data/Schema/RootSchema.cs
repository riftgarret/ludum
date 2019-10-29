using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data.Schema
{
	internal class RootSchema
	{
		public Type DataType { get; set; }
		public List<IDataSource> Data { get; set; }
	}
}
