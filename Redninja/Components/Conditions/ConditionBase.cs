using System.Collections.Generic;
using Redninja.Components.Combat.Events;
using Redninja.Components.Conditions.Expressions;

namespace Redninja.Components.Conditions
{
	internal class Condition : ICondition
	{
		// used for debugging
		public string Raw { get; set; }

		public IEnvExpression Left { get; protected set; }

		public IEnvExpression Right { get; protected set; }

		public IConditionalOperator Op { get; protected set; }

		public IOperatorCountRequirement OpRequirement { get; protected set; }

		public Condition(IEnvExpression left, IEnvExpression right, IConditionalOperator op, IOperatorCountRequirement req)
		{
			this.Left = left;
			this.Right = right;
			this.Op = op;
			this.OpRequirement = req;
		}

		public bool IsTargetConditionMet(IUnitModel self, IUnitModel target, IBattleModel battleModel)
			=> IsConditionMet(ExpressionEnv.From(battleModel, self, target));

		public bool IsEventConditionMet(IUnitModel self, ICombatEvent battleEvent, IBattleModel battleModel)
			=> IsConditionMet(ExpressionEnv.From(battleModel, self, battleEvent));


		private bool IsConditionMet(IExpressionEnv expressionEnv)
		{
			ExpressionResolver resolver = new ExpressionResolver(expressionEnv);

			IEnumerable<object> leftValues = resolver.Resolve(Left);
			IEnumerable<object> rightValues = resolver.Resolve(Right);

			ExpressionResultType resultType = Left.GetFinalResultType();
			IExpressionResultDef resultDef = ResultDefFactory.From(resultType);

			return Op.IsTrue(leftValues, rightValues, OpRequirement, resultDef);
		}
	}
}
