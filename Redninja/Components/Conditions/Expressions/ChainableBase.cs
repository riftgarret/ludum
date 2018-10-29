using System;
namespace Redninja.Components.Conditions.Expressions
{
	public abstract class ChainableBase : IChainedExpression, IChainableExpression
	{
		public ExpressionResultType Param { get; protected set;}

		public ExpressionResultType ResultType { get; protected set; }

		public IChainedExpression ChainedExpression { get; set; }

		public abstract object Result(object param);
	}
}
