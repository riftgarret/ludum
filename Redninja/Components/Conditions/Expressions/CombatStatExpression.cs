using System;
namespace Redninja.Components.Conditions.Expressions
{
	public class CombatStatExpression : ChainableBase
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
			=> 100 * model.Character.VolatileStats[CombatStat] / model.Character.Stats[CombatStat];

		public object Get(IUnitModel model) 
			=> IsPercent ? GetPercent(model) : model.Character.VolatileStats[CombatStat];

		public override object Result(object param) => Get((IUnitModel)param);
	}
}
