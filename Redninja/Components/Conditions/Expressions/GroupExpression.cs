using System;
using System.Collections.Generic;
using System.Linq;

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
			IEnumerable<int> values = (IEnumerable<int>)param;

			return Enumerable.Repeat(GetValue(values), 1);
		}

		private int GetValue(IEnumerable<int> values) 
		{
			switch (GroupOp)
			{
				case GroupOp.Avg:
					return (int) values.Average();

				case GroupOp.Highest:
					return values.Max();

				case GroupOp.Lowest:
					return values.Min();
				default:
					throw new InvalidProgramException($"group op case not implemented: {GroupOp}");
			}
		}
	}
}
