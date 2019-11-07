using System.Collections.Generic;
using System.Linq;
using System;

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
						   IOperatorCountRequirement requirement)
		{
			int total = left.Count() * right.Count();

			int numberTrue =
				(from lhs in left
				 from rhs in right
				 where Evaluate(lhs, rhs)
				 select lhs).Count();

			return requirement.MeetsRequirement(numberTrue, total);
		}

		private bool Evaluate(object lhs, object rhs)
		{
			switch (OperatorType)
			{
				case ConditionOperatorType.EQ:
					return lhs == rhs;
				case ConditionOperatorType.NEQ:
					return lhs != rhs;
			}

			if (lhs is float && rhs is float)
			{
				return EvaluateNumbers((float) lhs, (float) rhs);
			}

			throw new InvalidOperationException($"Could not find correct operator for: {lhs} and {rhs}");
		}

		private bool EvaluateNumbers(float lhs, float rhs)
		{
			switch(OperatorType)
			{
				case ConditionOperatorType.LT:
					return lhs < rhs;
				case ConditionOperatorType.LTE:
					return lhs <= rhs;
				case ConditionOperatorType.GT:
					return lhs > rhs;
				case ConditionOperatorType.GTE:
					return lhs >= rhs;
			}

			throw new InvalidOperationException("should never get here");
		}
	}
}
