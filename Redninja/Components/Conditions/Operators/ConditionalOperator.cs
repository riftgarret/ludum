using System.Collections.Generic;
using System.Linq;

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
						   IExpressionResultDef resultDef)
		{
			int total = left.Count() * right.Count();

			int numberTrue =
				(from lhs in left
				 from rhs in right
				 where resultDef.IsTrue(lhs, rhs, OperatorType)
				 select lhs).Count();

			return requirement.MeetsRequirement(numberTrue, total);
		}
	}
}
