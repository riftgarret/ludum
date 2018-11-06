using System;
namespace Redninja.Components.Conditions.ResultDefinitions
{
	public class IntResultDefinition : ResultDefinitionBase<int>
	{
		public IntResultDefinition(ExpressionResultType resultType)
			: base(resultType) { }

		public override bool CanSupportOperator(ConditionOperatorType operatorType) => true;

		protected override bool IsTrueCase(int lhs, int rhs, ConditionOperatorType opType)
		{
			switch (opType)
			{
				case ConditionOperatorType.EQ:
					return lhs == rhs;
				case ConditionOperatorType.NEQ:
					return lhs != rhs;
				case ConditionOperatorType.LT:
					return lhs < rhs;
				case ConditionOperatorType.LTE:
					return lhs <= rhs;
				case ConditionOperatorType.GT:
					return lhs > rhs;
				case ConditionOperatorType.GTE:
					return lhs >= rhs;
				default:
					throw new InvalidOperationException("should never get here");
			}
		}
	}
}
