using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Utils
{
	public static class LinqUtils
	{
		public static bool AreAll<TSource>(this IEnumerable<TSource> source, bool predRequirement, Func<TSource, bool> predicate) {
			return source.FirstOrDefault(t => predicate(t) != predRequirement) == default;
		}
	}
}
