using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data
{
	public interface IDataFactory
	{
		/// <summary>
		/// Create an instance of this data type.
		/// </summary>
		/// <typeparam name="TYPE"></typeparam>
		/// <param name="dataId"></param>
		/// <returns></returns>
		TYPE CreateInstance<TYPE>(string dataId) where TYPE : class;

		/// <summary>
		/// Get a cached version of this data type.
		/// </summary>
		/// <typeparam name="TYPE"></typeparam>
		/// <param name="dataId"></param>
		/// <returns></returns>
		TYPE SingleInstance<TYPE>(string dataId) where TYPE : class;
	}
}
