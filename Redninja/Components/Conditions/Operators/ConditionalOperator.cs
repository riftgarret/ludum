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
			int numRuns = 0;
			int numTrue = 0;

			IExpressionResultDef def = ResultDefFactory.From(resultType);

			if (!requirement.CanComplete(total))
				return false;

			foreach (object lhs in left)
			{
				foreach(object rhs in right) 
				{
					bool result = def.IsTrue(lhs, rhs, OperatorType);
					numRuns++;
					numTrue += result ? 1 : 0;

					if (requirement.TryComplete(numTrue, numRuns, total, out bool success))
						return success;
				}
			}

			RLog.E(this, $"Failed to find a resolution to compare left: {left}, right: {right}");
			return false;
		}
	}
}
