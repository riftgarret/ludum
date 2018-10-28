using System;
using System.Collections.Generic;
using Redninja.Components.Conditions.Expressions;
using System.Linq;

namespace Redninja.Components.Conditions
{
	internal class TargetCondition : ConditionBase
	{
		public bool IsTargetConditionMet(IUnitModel self, IUnitModel target, IBattleModel battleModel)
		{
			ExpressionResolver resolver = new ExpressionResolver(self, target, battleModel, null);

			IEnumerable<object> leftValues = resolver.Resolve(Left);
			IEnumerable<object> rightValues = resolver.Resolve(Right);

			ExpressionResultType resultType = Left.GetFinalResultType();

			return Op.IsTrue(leftValues, rightValues, OpRequirement, resultType);
		}
	}
}
