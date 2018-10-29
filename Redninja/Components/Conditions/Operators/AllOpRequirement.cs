using System;
namespace Redninja.Components.Conditions.Operators
{
	public class AllOpRequirement : IOperatorCountRequirement
	{
		public static readonly AllOpRequirement INSTANCE = new AllOpRequirement();

		public ConditionalOperatorRequirement RequirementType => ConditionalOperatorRequirement.All;

		public bool MeetsRequirement(int numberTrue, int total) => numberTrue == total;
	}
}
