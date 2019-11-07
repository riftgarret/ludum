using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Conditions.Expressions
{
	public struct ExpressionSplit
	{
		public string parsed;
		public string nextRaw;
	}

	public class NextExpressionMeta<T> 
	{
		public string Name { get; set; }
		public IExpression Expression { get; set; }
		public Func<IExpressionEnv, T, object> Extractor { get; set; }
	}

	public static class ExpressionExts
	{
		/// <summary>
		/// Ensure we have 2 indexes, even if the 2nd one is null.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="c"></param>
		/// <returns></returns>
		public static ExpressionSplit SplitOn(this string str, char c)
		{
			var split = str.Split(new char[] { c }, 2);
			return new ExpressionSplit()
			{
				parsed = split[0],
				nextRaw = split.Length == 1 ? null : split[1]
			};
		}
	}
}
