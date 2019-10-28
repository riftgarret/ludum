using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja
{
	public static class UtilExtensions
	{
		public static VALUE GetOrThrow<KEY, VALUE>(this Dictionary<KEY, VALUE> dict, KEY key, string dictName)
		{
			if (!dict.ContainsKey(key))
				throw new KeyNotFoundException($"Key: {key} not found in {dictName}");
			return dict[key];
		}
	}
}
