using System;
using Redninja.Components.Conditions.Expressions;
using Redninja.Components.Conditions.Operators;
using Redninja.Events;

namespace Redninja.Components.Conditions
{
	public interface ICondition
	{
		IInitialExpression Left { get; }
		IInitialExpression Right { get; }
		IConditionalOperator Op { get; }
		IOperatorCountRequirement OpRequirement { get; }
	}
}
