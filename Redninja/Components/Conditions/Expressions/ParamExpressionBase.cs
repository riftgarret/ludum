using System;
namespace Redninja.Components.Conditions.Expressions
{
	public abstract class ParamExpressionBase : IParamExpression
	{
		public ExpressionResultType Param { get; protected set;}

		public ExpressionResultType ResultType { get; protected set; }

		public IParamExpression Next { get; set; }

		public abstract object Result(object param);
	}
}
