namespace Redninja.Components.Conditions.Operators
{
	public class AllOpRequirement : IOperatorCountRequirement
	{
		public static AllOpRequirement Instance { get; } = new AllOpRequirement();

		public ConditionalOperatorRequirement RequirementType => ConditionalOperatorRequirement.All;

		public bool MeetsRequirement(int numberTrue, int total) => numberTrue == total && total != 0;
	}
}
