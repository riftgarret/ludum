using System;
namespace Redninja.Components.Conditions.Expressions
{
	public interface IChainedExpression : IExpression
	{
		ExpressionResultType Param { get; }
		object Result(object param);
	}
}
