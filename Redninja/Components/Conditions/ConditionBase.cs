using System;
using Redninja.Components.Conditions.Expressions;
using Redninja.Components.Conditions.Operators;
using Redninja.Events;

namespace Redninja.Components.Conditions
{
	internal abstract class ConditionBase : ICondition
	{
		// used for debugging
		public string Raw { get; set; }

		public IInitialExpression Left { get; protected set; }

		public IInitialExpression Right { get; protected set; }

		public IConditionalOperator Op { get; protected set; }

		public IOperatorCountRequirement OpRequirement { get; protected set; }
	}
}
