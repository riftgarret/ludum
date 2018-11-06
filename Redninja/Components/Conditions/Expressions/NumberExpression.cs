using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Conditions.Expressions
{
	public class NumberExpression : IEnvExpression
	{
		public NumberExpression(int result, bool percent = false)
		{
			Result = result;
			IsPercent = percent;
			ResultType = percent ? ExpressionResultType.Percent : ExpressionResultType.IntValue;
		}

		public ExpressionResultType ResultType { get; }

		public bool IsPercent { get; }

		public object Result { get; }

		public IParamExpression Next
		{
			get => null;
			set => throw new InvalidOperationException("Cannot set string expression next");
		}

		public IEnumerable<object> GetResult(IExpressionEnv env) => Enumerable.Repeat(Result, 1);

		public override string ToString() => $"{Result}";
	}
}
