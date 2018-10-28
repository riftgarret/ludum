using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Conditions.Expressions
{
	public class TargetUnitExpression : ITargetUnitExpression, IInitialExpression
	{
		public TargetUnitExpression(ConditionTargetType targetType)
		{
			TargetType = targetType;
		}

		public ConditionTargetType TargetType { get; }

		public ExpressionResultType ResultType => ExpressionResultType.Unit;

		public IChainableExpression ChainedExpression { get; set; }

		public IEnumerable<IUnitModel> Result(IUnitModel self, IUnitModel target, IBattleModel battleModel)
		{
			switch(TargetType)
			{
				case ConditionTargetType.All:
					return battleModel.Entities;
				case ConditionTargetType.Ally:
					return battleModel.Entities.Where(x => x.Team == self.Team);
				case ConditionTargetType.AllyNotSelf:
					return battleModel.Entities.Where(x => x.Team == self.Team && x != self);
				case ConditionTargetType.Enemy:
					return battleModel.Entities.Where(x => x.Team != self.Team);
				case ConditionTargetType.Self:
					return Enumerable.Repeat(self, 1);
				case ConditionTargetType.Target:
					return Enumerable.Repeat(target, 1);
				default:
					throw new InvalidOperationException($"Unsupported target type: {TargetType}");
			}
		}
	}
}
