using System.Collections.Generic;

namespace Redninja.Components.Conditions.Expressions
{
	internal class ExpressionResolver
	{
		private IExpressionEnv env;

		public ExpressionResolver(IExpressionEnv env)
		{
			this.env = env;
		}

		public IEnumerable<object> Resolve(IEnvExpression expression)
		{
			IEnumerable<object> result = expression.GetResult(env);
			IParamExpression chainableExpression = expression.Next;

			while(chainableExpression != null)
			{
				result = ResolveChain(chainableExpression, result);
				chainableExpression = chainableExpression.Next;
			}

			return result;
		}

		internal IEnumerable<object> ResolveChain(IParamExpression expression, IEnumerable<object> paramList)
		{
			List<object> results = new List<object>();

			// if group expression evaluate accordingly to group up the results.
			if (expression is IGroupExpression)
			{
				results.Add(((IGroupExpression)expression).GroupResult(paramList));
			}
			else
			{
				foreach (object param in paramList)
				{
					results.Add(expression.Result(param));
				}
			}

			return results;
		}
	}
}
