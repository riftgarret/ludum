using Redninja.Components.Decisions.AI;

namespace Redninja.Components.Conditions.Expressions
{
	public class StatExpression : ParamExpressionBase
	{
		public StatExpression(IStatEvaluator statEvaluator)
		{
			StatEvaluator = statEvaluator;
			bool isPercent = statEvaluator is LiveStatEvaluator
				&& ((LiveStatEvaluator)statEvaluator).IsPercent;

			ResultType = isPercent ? ExpressionResultType.Percent : ExpressionResultType.IntValue;
			Param = ExpressionResultType.Unit;
		}

		public ConditionTargetType TargetType { get; }
		public IStatEvaluator StatEvaluator { get; }				
		public object Get(IBattleEntity model) => StatEvaluator.Eval(model);		

		public override object Result(object param) => Get((IBattleEntity)param);
	}
}
