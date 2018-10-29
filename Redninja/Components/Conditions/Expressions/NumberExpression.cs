using System;
namespace Redninja.Components.Conditions.Expressions
{
	public class NumberExpression : IValueExpression
	{
		public NumberExpression(int result, bool percent = false)
		{
			Result = result;
			ResultType = percent ? ExpressionResultType.Percent : ExpressionResultType.IntValue;
		}

		public ExpressionResultType ResultType { get; }

		public object Result { get; }

		public IChainedExpression ChainedExpression => null;

		public override string ToString() => $"{Result}";
	}
}
