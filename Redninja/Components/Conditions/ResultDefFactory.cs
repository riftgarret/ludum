using System;
using Redninja.Components.Conditions.ResultDefitions;

namespace Redninja.Components.Conditions
{
	public static class ResultDefFactory
	{
		public static IExpressionResultDef IntValueResult { get; } = new IntResultDefinition(ExpressionResultType.IntValue);
		public static IExpressionResultDef PercentValueResult { get; } = new IntResultDefinition(ExpressionResultType.Percent);
		public static IExpressionResultDef ClassNameResult { get; } = new StringResultDefinition(ExpressionResultType.ClassName);
		public static IExpressionResultDef UnitResult { get; } = new ObjectResultDefinition<IUnitModel>(ExpressionResultType.Unit);
		public static IExpressionResultDef BattleResult { get; } = new ObjectResultDefinition<IBattleModel>(ExpressionResultType.Battle);

		public static IExpressionResultDef From(ExpressionResultType resultType)
		{
			switch(resultType)
			{
				case ExpressionResultType.Battle:
					return BattleResult;
				case ExpressionResultType.ClassName:
					return ClassNameResult;
				case ExpressionResultType.IntValue:
					return IntValueResult;
				case ExpressionResultType.Percent:
					return PercentValueResult;
				case ExpressionResultType.Unit:
					return UnitResult;

				default:
					throw new InvalidOperationException($"Missing implementation for resultType: {resultType}");
			}
		}
	}
}
