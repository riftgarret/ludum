using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Conditions.Expressions
{
	public class TargetUnitExpression : IEnvExpression
	{
		public TargetUnitExpression(ConditionTargetType targetType)
		{
			TargetType = targetType;
		}

		public ConditionTargetType TargetType { get; }

		public ExpressionResultType ResultType => ExpressionResultType.Unit;

		public IParamExpression Next { get; set; }

		public IEnumerable<object> GetResult(IExpressionEnv env)
		{
			return GetTargetResult(env);
		}

		public IEnumerable<IBattleEntity> GetTargetResult(IExpressionEnv env)
		{
			switch (TargetType)
			{
				case ConditionTargetType.Any:
					return env.BattleModel.Entities;
				case ConditionTargetType.Ally:
					return env.BattleModel.Entities.Where(x => x.Team == env.Self.Team);
				case ConditionTargetType.AllyNotSelf:
					return env.BattleModel.Entities.Where(x => x.Team == env.Self.Team && x != env.Self);
				case ConditionTargetType.Enemy:
					return env.BattleModel.Entities.Where(x => x.Team != env.Self.Team);
				case ConditionTargetType.Self:
					return Enumerable.Repeat(env.Self, 1);
				case ConditionTargetType.Target:
					return Enumerable.Repeat(env.Target, 1);
				case ConditionTargetType.Source:
					return Enumerable.Repeat(env.Source, 1);
				default:
					throw new InvalidOperationException($"Unsupported target type: {TargetType}");
			}
		}
	}
}
