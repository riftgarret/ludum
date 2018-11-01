using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Conditions.Expressions
{
	public class StringExpression : IEnvExpression
	{
		public StringExpression(string result)
		{
			Result = result;
		}

		public virtual ExpressionResultType ResultType => ExpressionResultType.ClassName;

		public IParamExpression Next
		{
			get => null;
			set => throw new InvalidOperationException("Cannot set string expression next");
		}

		public object Result { get; }

		public IEnumerable<object> GetResult(IExpressionEnv env) => Enumerable.Repeat(Result, 1);
	}
}
