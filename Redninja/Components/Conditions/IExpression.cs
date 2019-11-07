using Redninja.Components.Conditions.Expressions;

namespace Redninja.Components.Conditions
{
	public interface IExpression
	{
		ExpressionResultType ResultType { get; }
		object GetResult(IExpressionEnv env, object param);
	}
}
