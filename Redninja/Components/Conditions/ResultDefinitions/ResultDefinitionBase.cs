using System;
using System.Collections.Generic;

namespace Redninja.Components.Conditions.ResultDefinitions
{
	public abstract class ResultDefinitionBase<T> : IExpressionResultDef
	{
		protected ResultDefinitionBase(ExpressionResultType resultType)
		{
			ResultType = resultType;
			NativeType = typeof(T);
		}

		public ExpressionResultType ResultType { get; }

		public Type NativeType { get; }

		public abstract bool CanSupportOperator(ConditionOperatorType operatorType);

		public bool IsTrue(object lhs, object rhs, ConditionOperatorType opType)
			=> IsTrueCase((T)lhs, (T)rhs, opType);

		protected abstract bool IsTrueCase(T lhs, T rhs, ConditionOperatorType opType);

		public override bool Equals(object obj)
		{
			var @base = obj as ResultDefinitionBase<T>;
			return @base != null &&
				   ResultType == @base.ResultType &&
				   EqualityComparer<Type>.Default.Equals(NativeType, @base.NativeType);
		}

		public override int GetHashCode()
		{
			var hashCode = -769100618;
			hashCode = hashCode * -1521134295 + ResultType.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(NativeType);
			return hashCode;
		}
	}
}
