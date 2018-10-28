using System;
namespace Redninja.Components.Conditions.Operators
{
	/// <summary>
	/// Operator Count Requirement.
	/// </summary>
	public interface IOperatorCountRequirement
	{
		ConditionalOperatorRequirement RequirementType { get; }

		/// <summary>
		/// Can this operation actually resolve to true or false? We can check this ahead of time.
		/// </summary>
		/// <returns><c>true</c>, if complete was caned, <c>false</c> otherwise.</returns>
		/// <param name="total">Total.</param>
		bool CanComplete(int total);
		bool TryComplete(int numberTrue, int numberRan, int total, out bool result);
	}
}
