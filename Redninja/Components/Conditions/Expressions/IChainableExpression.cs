using System;
namespace Redninja.Components.Conditions.Expressions
{
	public interface IChainableExpression : IExpression
	{
		new IChainedExpression ChainedExpression { get; set; }
	}
}
