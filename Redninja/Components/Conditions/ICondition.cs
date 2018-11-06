namespace Redninja.Components.Conditions
{
	public interface ICondition
	{
		IEnvExpression Left { get; }
		IEnvExpression Right { get; }
		IConditionalOperator Op { get; }
		IOperatorCountRequirement OpRequirement { get; }
	}
}
