using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Logging;

namespace Redninja.Components.Conditions.Operators
{
	public class ConditionalOperator : IConditionalOperator
	{
		public ConditionalOperator(ConditionOperatorType opType)
		{
			OperatorType = opType;
		}

		public ConditionOperatorType OperatorType { get; }

		public bool IsTrue(IEnumerable<object> left,
						   IEnumerable<object> right,
						   IOperatorCountRequirement requirement,
						   ExpressionResultType resultType)
		{
			int total = left.Count() * right.Count();

			IExpressionResultDef def = ResultDefFactory.From(resultType);

			int numberTrue =
				(from lhs in left
				from rhs in right
				where def.IsTrue(lhs, rhs, OperatorType)
				 select lhs).Count();

			return requirement.MeetsRequirement(numberTrue, total);
		}
	}
}
