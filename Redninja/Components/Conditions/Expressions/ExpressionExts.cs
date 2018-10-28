using System;
namespace Redninja.Components.Conditions.Expressions
{
	public static class ExpressionExts
	{
		public static ExpressionResultType GetFinalResultType(this IExpression expression)
		{
			IExpression cur = expression;
			while (cur.ChainedExpression != null) cur = cur.ChainedExpression;
			return cur.ResultType;
		}
	}
}
