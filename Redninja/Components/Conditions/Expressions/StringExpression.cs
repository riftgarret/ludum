using System;
namespace Redninja.Components.Conditions.Expressions
{
	public class StringExpression : IValueExpression
	{
		public StringExpression(string result)
		{
			Result = result;
		}

		public virtual ExpressionResultType ResultType => ExpressionResultType.ClassName;

		public IChainableExpression ChainedExpression => null;

		public object Result { get; }
	}
}
