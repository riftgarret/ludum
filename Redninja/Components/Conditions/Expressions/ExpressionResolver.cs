using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Events;

namespace Redninja.Components.Conditions.Expressions
{
	internal class ExpressionResolver
	{
		private readonly IUnitModel self;
		private readonly IUnitModel target;
		private readonly IBattleModel battleModel;
		private readonly IBattleEvent battleEvent;

		public ExpressionResolver(IUnitModel self, IUnitModel target, IBattleModel battleModel, IBattleEvent battleEvent)
		{
			this.self = self;
			this.target = target;
			this.battleModel = battleModel;
			this.battleEvent = battleEvent;
		}

		public IEnumerable<object> Resolve(IInitialExpression expression)
		{
			IEnumerable<object> result = ResolveInitialExpression(expression);
			IChainedExpression chainableExpression = expression.ChainedExpression;

			while(chainableExpression != null)
			{
				result = ResolveChain(chainableExpression, result);
				chainableExpression = chainableExpression.ChainedExpression;
			}

			return result;
		}

		internal IEnumerable<object> ResolveInitialExpression(IInitialExpression expression)
		{
			if (expression is ITargetUnitExpression) return ResolveUnits((ITargetUnitExpression)expression);
			if (expression is IValueExpression) return ResolveValue((IValueExpression)expression);

			throw new InvalidOperationException($"Invalid initial expression: {expression}");
		}

		internal IEnumerable<object> ResolveChain(IChainedExpression expression, IEnumerable<object> paramList)
		{
			List<object> results = new List<object>();

			foreach(object param in paramList)
			{
				results.Add(expression.Result(param));
			}

			return results;
		}

		internal IEnumerable<object> ResolveValue(IValueExpression valueExpression)
			=> Enumerable.Repeat(valueExpression.Result, 1);

		internal IEnumerable<object> ResolveUnits(ITargetUnitExpression expression) 
			=> expression.Result(self, target, battleModel);
	}
}
