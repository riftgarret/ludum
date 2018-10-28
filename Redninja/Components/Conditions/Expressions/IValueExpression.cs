using System;
namespace Redninja.Components.Conditions.Expressions
{
	public interface IValueExpression : IInitialExpression
	{
		object Result { get; }
	}
}
