namespace Redninja.Components.Conditions.Expressions
{
	public interface IParamExpression : IExpression
	{
		ExpressionResultType Param { get; }
		object Result(object param);
	}
}
