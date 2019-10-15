namespace Redninja.Components.Conditions.Expressions
{
	public class CombatStatExpression : ParamExpressionBase
	{
		public CombatStatExpression(Stat combatStat, bool isPercent)
		{
			CombatStat = combatStat;
			IsPercent = isPercent;
			ResultType = IsPercent ? ExpressionResultType.Percent : ExpressionResultType.IntValue;
			Param = ExpressionResultType.Unit;
		}

		public ConditionTargetType TargetType { get; }
		public Stat CombatStat { get; }
		public bool IsPercent { get; }

		private int GetPercent(IBattleEntity model)
			=> 100 * model.VolatileStats[CombatStat] / model.Stats[CombatStat];

		public object Get(IBattleEntity model)
			=> IsPercent ? GetPercent(model) : model.VolatileStats[CombatStat];

		public override object Result(object param) => Get((IBattleEntity)param);
	}
}
