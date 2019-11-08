using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Conditions.Expressions
{
	public class NumberExpression : IExpression
	{
		public NumberExpression()
		{
			
		}

		public ExpressionResultType ResultType { get; } = ExpressionResultType.IntValue;

		public object GetResult(IExpressionEnv env, object number) => (float) number;		
	}
}
