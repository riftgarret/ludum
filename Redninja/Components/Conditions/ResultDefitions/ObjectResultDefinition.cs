using System;
namespace Redninja.Components.Conditions.ResultDefitions
{
	public class ObjectResultDefinition<T> : ResultDefinitionBase<T> where T : class
	{
		public ObjectResultDefinition(ExpressionResultType resultType)
			: base(resultType) { }

		public override bool CanSupportOperator(ConditionOperatorType operatorType)
		{
			switch (operatorType)
			{
				case ConditionOperatorType.EQ:
				case ConditionOperatorType.NEQ:
					return true;
				default:
					return false;
			}
		}

		protected override bool IsTrueCase(T lhs, T rhs, ConditionOperatorType opType)
		{
			switch (opType)
			{
				case ConditionOperatorType.EQ:
					return Equals(lhs, rhs);
				case ConditionOperatorType.NEQ:
					return !Equals(lhs, rhs);
				default:
					throw new InvalidOperationException($"Cannot support operator for string: {opType}");
			}
		}
	}
}
