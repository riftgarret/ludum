using System.Collections.Generic;

namespace Redninja.Components.Conditions.Expressions
{
	public interface IGroupExpression : IParamExpression
	{
		object GroupResult(IEnumerable<object> param);
	}
}
