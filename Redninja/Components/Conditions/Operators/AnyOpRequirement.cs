using System;
namespace Redninja.Components.Conditions.Operators
{
	public class AnyOpRequirement : IOperatorCountRequirement
	{
		public static readonly AnyOpRequirement INSTANCE = new AnyOpRequirement();

		public ConditionalOperatorRequirement RequirementType => ConditionalOperatorRequirement.Any;

		public bool MeetsRequirement(int numberTrue, int total) => numberTrue >= 1;
	}
}
