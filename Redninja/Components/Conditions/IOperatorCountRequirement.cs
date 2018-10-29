using System;
namespace Redninja.Components.Conditions
{
	/// <summary>
	/// Operator Count Requirement.
	/// </summary>
	public interface IOperatorCountRequirement
	{
		ConditionalOperatorRequirement RequirementType { get; }

		bool MeetsRequirement(int numberTrue, int total);
	}
}
