using System;
using System.Collections.Generic;

namespace Redninja.Components.Conditions.Operators
{
	public class ConditionalOperator : IConditionalOperator
	{
		public ConditionalOperator()
		{
		}

		public bool IsTrue(IEnumerable<object> left, 
		                   IEnumerable<object> right, 
		                   IOperatorCountRequirement requirement,
						   ExpressionResultType resultType)
		{
			throw new NotImplementedException();
		}
	}
}
