using System;
namespace Redninja.Components.Conditions.Expressions
{
	public abstract class ChainableBase : IChainableExpression
	{
		public ExpressionResultType Param { get; protected set;}

		public ExpressionResultType ResultType { get; protected set; }

		public IChainableExpression ChainedExpression { get; set; }

		public abstract object Result(object param);
	}
}
