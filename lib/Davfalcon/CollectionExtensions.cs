using System.Collections.Generic;

namespace Davfalcon
{
	public static class CollectionExtensions
	{
		public static T LastOrDefault<T>(this IList<T> list) => list.Count > 0 ? list[list.Count - 1] : default;
	}
}
