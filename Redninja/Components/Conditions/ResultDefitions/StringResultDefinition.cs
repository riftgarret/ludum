using System;
namespace Redninja.Components.Conditions.ResultDefitions
{
	public class StringResultDefinition : ResultDefinitionBase<string>
	{
		public StringResultDefinition(ExpressionResultType resultType)
			: base(resultType) { }

		public override bool CanSupportOperator(ConditionOperatorType operatorType)
		{
			switch(operatorType)
			{
				case ConditionOperatorType.EQ:
				case ConditionOperatorType.NEQ:
					return true;
				default:
					return false;
			}
		}

		protected override bool IsTrueCase(string lhs, string rhs, ConditionOperatorType opType)
		{
			switch(opType)
			{
				case ConditionOperatorType.EQ:
					return String.Equals(lhs, rhs);
				case ConditionOperatorType.NEQ:
					return !String.Equals(lhs, rhs);	
				default:
					throw new InvalidOperationException($"Cannot support operator for string: {opType}");
			}
		}
	}
}
