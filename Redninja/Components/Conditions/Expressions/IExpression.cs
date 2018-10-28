using System;
namespace Redninja.Components.Conditions.Expressions
{
	public interface IExpression
	{
		ExpressionResultType ResultType { get; }
		IChainableExpression ChainedExpression { get; }
	}
}
