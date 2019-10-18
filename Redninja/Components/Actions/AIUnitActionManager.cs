using System;
using Redninja.Components.Decisions.AI;

namespace Redninja.Components.Actions
{
	[Serializable]
	public class AIUnitActionManager : UnitActionManager
	{
		public IAIBehavior AIBehavior { get; }

		public AIUnitActionManager(IBattleContext context, IBattleEntity entity, AIRuleSet ruleSet) : base(context, entity)
		{
			AIBehavior = ruleSet != null ? new AIBehavior(BattleContext, BattleEntity, ruleSet) : null;
		}
	}
}
