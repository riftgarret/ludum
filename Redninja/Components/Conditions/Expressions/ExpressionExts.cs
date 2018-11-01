using System;
namespace Redninja.Components.Conditions.Expressions
{
	public static class ExpressionExts
	{
		public static ExpressionResultType GetFinalResultType(this IExpression expression)
		{
			IExpression cur = expression;
			while (cur.Next != null) cur = cur.Next;
			return cur.ResultType;
		}
	}
}
