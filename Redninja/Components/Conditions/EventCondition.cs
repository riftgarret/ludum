using System;
using System.Collections.Generic;
using Redninja.Components.Conditions.Expressions;
using Redninja.Events;

namespace Redninja.Components.Conditions
{
	internal class EventCondition : ConditionBase
	{
		public EventCondition(IInitialExpression left, IInitialExpression right, IConditionalOperator op, IOperatorCountRequirement req)
		{
			this.Left = left;
			this.Right = right;
			this.Op = op;
			this.OpRequirement = req;
		}

		public bool IsTargetConditionMet(IUnitModel self, IBattleEvent battleEvent, IBattleModel battleModel)
		{
			IUnitModel target = battleEvent is ITargetedEvent ? ((ITargetedEvent)battleEvent).Target : null;
			ExpressionResolver resolver = new ExpressionResolver(self, target, battleModel, battleEvent);

			IEnumerable<object> leftValues = resolver.Resolve(Left);
			IEnumerable<object> rightValues = resolver.Resolve(Right);

			ExpressionResultType resultType = Left.GetFinalResultType();

			return Op.IsTrue(leftValues, rightValues, OpRequirement, resultType);
		}
	}
}
