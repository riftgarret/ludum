using System;
namespace Redninja.Components.Conditions
{
	public interface IExpressionResultDef
	{
		ExpressionResultType ResultType { get; }

		Type NativeType { get; }

		bool CanSupportOperator(ConditionOperatorType operatorType);

		bool IsTrue(object lhs, object rhs, ConditionOperatorType opType);
	}
}
