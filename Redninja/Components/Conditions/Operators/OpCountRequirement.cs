using System;

namespace Redninja.Components.Conditions.Operators
{
	public class OpCountRequirement : IOperatorCountRequirement
	{
		public OpCountRequirement(ConditionOperatorType opType, int count)
		{
			OpType = opType;
			Count = count;
		}

		public ConditionalOperatorRequirement RequirementType => ConditionalOperatorRequirement.CountOp;

		public ConditionOperatorType OpType { get; }
		public int Count { get; }

		public bool MeetsRequirement(int numberTrue, int total)
		{
			switch (OpType)
			{
				case ConditionOperatorType.EQ:
					return numberTrue == Count;
				case ConditionOperatorType.NEQ:
					return numberTrue != Count;
				case ConditionOperatorType.LT:
					return numberTrue < Count;
				case ConditionOperatorType.LTE:
					return numberTrue <= Count;
				case ConditionOperatorType.GT:
					return numberTrue > Count;
				case ConditionOperatorType.GTE:
					return numberTrue >= Count;
				default:
					throw new InvalidOperationException("should never get here");
			}
		}
	}
}
