﻿namespace Redninja.Components.Conditions.Expressions
{
	public class CombatStatExpression : ParamExpressionBase
	{
		public CombatStatExpression(CombatStats combatStat, bool isPercent)
		{
			CombatStat = combatStat;
			IsPercent = isPercent;
			ResultType = IsPercent ? ExpressionResultType.Percent : ExpressionResultType.IntValue;
			Param = ExpressionResultType.Unit;
		}

		public ConditionTargetType TargetType { get; }
		public CombatStats CombatStat { get; }
		public bool IsPercent { get; }

		private int GetPercent(IUnitModel model)
			=> 100 * model.VolatileStats[CombatStat] / model.VolatileStats[CombatStat];

		public object Get(IUnitModel model)
			=> IsPercent ? GetPercent(model) : model.VolatileStats[CombatStat];

		public override object Result(object param) => Get((IUnitModel)param);
	}
}
