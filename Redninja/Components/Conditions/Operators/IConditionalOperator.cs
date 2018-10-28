using System;
using System.Collections.Generic;

namespace Redninja.Components.Conditions.Operators
{
	public interface IConditionalOperator
	{
		bool IsTrue(IEnumerable<object> left, 
		            IEnumerable<object> right, 
		            IOperatorCountRequirement requirement,
		            ExpressionResultType resultType);
	}
}
