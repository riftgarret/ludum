using System;
namespace Redninja.Components.Conditions.Operators
{
	public class AllOpRequirement : IOperatorCountRequirement
	{
		public static readonly AllOpRequirement INSTANCE = new AllOpRequirement();

		public ConditionalOperatorRequirement RequirementType => ConditionalOperatorRequirement.All;

		public bool CanComplete(int total) => total > 0;

		public bool TryComplete(int numberTrue, int numberRan, int total, out bool result)
		{
			result = false;
			if (numberTrue < numberRan)
			{
				return true;
			}
			if (numberTrue == total)
			{
				result = true;
				return true;
			}

			return false;
		}
	}
}
