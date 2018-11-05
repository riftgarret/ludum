using System;
using Redninja.Components.Conditions;
using Redninja.Components.Conditions.Operators;

namespace Redninja.Data.Schema.Readers
{
	internal class ConditionOpParser
	{
		internal bool TryParseOp(string raw, out IConditionalOperator op)
		{
			op = null;
			if (!TryParseOperatorType(raw, out ConditionOperatorType type))
				return false;

			op = new ConditionalOperator(type);
			return true;
		}

		internal static bool TryParseOperatorType(string raw, out ConditionOperatorType op)
		{
			switch (raw)
			{
				case ">":
					op = ConditionOperatorType.GT;
					return true;
				case ">=":
					op = ConditionOperatorType.GTE;
					return true;
				case "<":
					op = ConditionOperatorType.LT;
					return true;
				case "<=":
					op = ConditionOperatorType.LTE;
					return true;
				case "!=":
					op = ConditionOperatorType.NEQ;
					return true;
				case "=":
					op = ConditionOperatorType.EQ;
					return true;
				default:
					op = ConditionOperatorType.EQ;
					return false;
			}
		}
	}
}
