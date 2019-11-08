using System;
using Redninja.Components.Combat.Events;
using Redninja.Components.Conditions.Expressions;

namespace Redninja.Components.Conditions
{
	public interface ICondition
	{
		IExpression Left { get; }
		IExpression Right { get; }
		IConditionalOperator Op { get; }
		IOperatorCountRequirement OpRequirement { get; }

		bool IsConditionMet(IExpressionEnv env);
		bool IsConditionMet(Action<ExpressionEnv.ExpressionEnvBuilder> builderFunc);		
	}
}
