namespace Redninja.Components.Skills.StatusEffects
{
	public interface IConditionalEffect
	{
		IStateCondition StateCondition { get; }
		bool IsActive { get; }
	}
}
