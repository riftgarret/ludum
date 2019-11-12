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
			if(lhs.GetType() != rhs.GetType())
				throw new InvalidOperationException($"Incompatable types: Could not find correct operator for: {lhs} and {rhs}");			

			if (lhs is IComparable)
				return EvaluateComparable((IComparable)lhs, (IComparable)rhs);

			
			switch (OperatorType)
			{
				case ConditionOperatorType.EQ:
					return lhs == rhs;
				case ConditionOperatorType.NEQ:				
					return lhs != rhs;
			}
			
			throw new InvalidOperationException($"Could not find correct operator for: {lhs} and {rhs}");
		}

		private bool EvaluateComparable(IComparable lhs, IComparable rhs)
		{
			int compareValue = lhs.CompareTo(rhs);

			switch (OperatorType)
			{
				case ConditionOperatorType.LT:
					return compareValue < 0;
				case ConditionOperatorType.LTE:
					return compareValue <= 0;
				case ConditionOperatorType.GT:
					return compareValue > 0;
				case ConditionOperatorType.GTE:
					return compareValue >= 0;
				case ConditionOperatorType.EQ:
					return compareValue == 0;
				case ConditionOperatorType.NEQ:
				default:
					return compareValue != 0;
			}
		}
	}
}
