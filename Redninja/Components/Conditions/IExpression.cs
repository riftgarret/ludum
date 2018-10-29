using System;
using Redninja.Components.Conditions.Expressions;

namespace Redninja.Components.Conditions
{
	public interface IExpression
	{
		ExpressionResultType ResultType { get; }
		IChainedExpression ChainedExpression { get; }
	}
}
