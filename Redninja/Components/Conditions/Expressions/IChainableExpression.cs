using System;
namespace Redninja.Components.Conditions.Expressions
{
	public interface IChainableExpression : IExpression
	{
		ExpressionResultType Param { get; }
		object Result(object param);
	}
}
