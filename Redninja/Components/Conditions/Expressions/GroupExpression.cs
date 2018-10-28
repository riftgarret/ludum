using System;
using System.Collections.Generic;

namespace Redninja.Components.Conditions.Expressions
{
	public class GroupExpression : ChainableBase, IGroupExpression
	{
		public GroupExpression(GroupOp groupOp, ExpressionResultType resultType)
		{
			GroupOp = groupOp;
			Param = resultType;
			ResultType = resultType;
		}

		public GroupOp GroupOp { get; }

		public override object Result(object param)
		{
			IEnumerable<int> p = (IEnumerable<int>)param;

		}
	}
}
