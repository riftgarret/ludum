using System.Collections.Generic;

namespace Redninja.Components.Conditions
{
	public interface IEnvExpression : IExpression
	{
		IEnumerable<object> GetResult(IExpressionEnv env);
	}
}
