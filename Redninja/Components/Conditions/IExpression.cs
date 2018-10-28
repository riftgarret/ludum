using System;
namespace Redninja.Components.Conditions
{
	public interface IExpression
	{
		ExpressionResultType ResultType { get; }
		IChainableExpression ChainedExpression { get; }
	}
}
