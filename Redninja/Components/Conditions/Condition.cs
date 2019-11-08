using System.Collections.Generic;
using Redninja.Components.Combat.Events;
using Redninja.Components.Conditions.Expressions;
using System.Linq;
using System;

namespace Redninja.Components.Conditions
{
	internal class Condition : ICondition
	{
		// used for debugging
		public string Raw { get; set; }

		public IExpression Left { get; protected set; }

		public IExpression Right { get; protected set; }

		public IConditionalOperator Op { get; protected set; }

		public IOperatorCountRequirement OpRequirement { get; protected set; }

		public Condition(IExpression left, IExpression right, IConditionalOperator op, IOperatorCountRequirement req)
		{
			this.Left = left;
			this.Right = right;
			this.Op = op;
			this.OpRequirement = req;
		}


		public bool IsConditionMet(Action<ExpressionEnv.ExpressionEnvBuilder> builderFunc)
		{
			var builder = new ExpressionEnv.ExpressionEnvBuilder();			
			builderFunc?.Invoke(builder);			
			return IsConditionMet(builder.Build());
		}		

		public bool IsConditionMet(IExpressionEnv expressionEnv)
		{
			var leftValue = Left.GetResult(expressionEnv, expressionEnv);			
			var rightValue = Right.GetResult(expressionEnv, expressionEnv);			
					
			return Op.IsTrue(AsEnumerable(leftValue), AsEnumerable(rightValue), OpRequirement);
		}

		private IEnumerable<object> AsEnumerable(object val)
		{
			return (val is IEnumerable<object>) ? (IEnumerable<object>)val : Enumerable.Repeat(val, 1);
		}
	}
}
