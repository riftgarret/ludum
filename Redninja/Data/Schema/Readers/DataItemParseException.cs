using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data.Schema.Readers
{
	internal class DataItemParseException : Exception
	{
		private DataItemParseException(string msg, Exception inner) : base(msg, inner)
		{

		}

		public static DataItemParseException ExceptionFrom<T>(IDataItemFactory<T> factory, string itemId, Exception inner) where T : class
		{
			string message = $"Parse Exception: [{factory.GetType().Name}, {itemId}]";
			return new DataItemParseException(message, inner);
		}
	}
}
