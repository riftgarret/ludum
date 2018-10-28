using System;
namespace Redninja.Components.Conditions.Operators
{
	public class AnyOpRequirement : IOperatorCountRequirement
	{
		public ConditionalOperatorRequirement RequirementType => ConditionalOperatorRequirement.Any;

		public bool CanComplete(int total) => total > 0;

		public bool TryComplete(int numberTrue, int numberRan, int total, out bool result)
		{
			result = false;
			if(numberTrue > 0) 
			{
				result = true;
				return true;
			}
			if(numberRan == total && numberTrue == 0)
			{
				return true;
			}

			return false;
		}
	}
}
