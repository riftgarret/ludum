using Redninja.Components.Combat.Events;

namespace Redninja.Components.Conditions
{
	public interface ICondition
	{
		IExpression Left { get; }
		IExpression Right { get; }
		IConditionalOperator Op { get; }
		IOperatorCountRequirement OpRequirement { get; }

		bool IsTargetConditionMet(IBattleEntity self, IBattleEntity target, IBattleModel battleModel);
		bool IsEventConditionMet(IBattleEntity self, ICombatEvent battleEvent, IBattleModel battleModel);
	}
}
