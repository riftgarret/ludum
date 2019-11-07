using System.Collections.Generic;

namespace Redninja.Components.Conditions
{
	public interface IConditionalOperator
	{
		ConditionOperatorType OperatorType { get; }

		bool IsTrue(IEnumerable<object> left,
					IEnumerable<object> right,
					IOperatorCountRequirement requirement);
	}
}
