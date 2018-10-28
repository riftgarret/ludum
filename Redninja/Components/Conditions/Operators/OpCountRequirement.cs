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

		public bool CanComplete(int total)
		{
			switch(OpType)
			{
				case ConditionOperatorType.EQ:
					return total >= Count;
				case ConditionOperatorType.GT:
					return total > Count;
				case ConditionOperatorType.GTE:
					return total >= Count;
				default:
					return true; // all other cases can be achieved regardless of total, even if 0
			}
		}

		public bool TryComplete(int numberTrue, int numberRan, int total, out bool result)
		{
			result = false;
			switch(OpType)
			{
				case ConditionOperatorType.EQ:
					if (numberTrue == Count && numberRan == total)
					{
						result = true;
						return true;
					}
					else if(numberTrue > Count
					       || total - numberRan < Count - numberTrue) // not enough passes left
					{
						return true;
					}
					break;
				case ConditionOperatorType.NEQ:
					if((numberTrue != Count && numberRan == total)
					   || numberTrue > Count
					   || total - numberRan < Count - numberTrue) // not enough passes left to make equality
					{
						result = true;
						return true;
					}
					break;
				case ConditionOperatorType.GT:
					if (numberTrue > Count)
					{
						result = true;
						return true;
					}
					else if(total - numberRan <= Count - numberTrue)
					{
						return true;
					}
					break;
				case ConditionOperatorType.GTE:
					if(numberTrue >= Count)
					{
						result = true;
						return true;
					}
					else if(total - numberRan < Count - numberTrue)
					{
						result = true;
					}
					break;
				case ConditionOperatorType.LT:
					if(total - Count < numberRan - numberTrue)
					{
						result = true;
						return true;
					}
					else if(numberTrue >= Count)
					{
						return true;
					}
					break;
				case ConditionOperatorType.LTE:
					if (total - Count <= numberRan - numberTrue)
					{
						result = true;
						return true;
					}
					else if (numberTrue > Count)
					{
						return true;
					}
					break;

			}


			return numberRan != total;
		}
	}
}
