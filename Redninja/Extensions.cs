using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja
{
	public static class Extensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			foreach (T item in enumeration)
			{
				action(item);
			}
		}

		public static V GetOrNew<K, V>(this IDictionary<K, V> dict, K key, Func<V> newObjFunc)
		{
			if(dict.ContainsKey(key))
			{
				return dict[key];				
			} else
			{
				return dict[key] = newObjFunc.Invoke();				
			}
		}
	}
}
