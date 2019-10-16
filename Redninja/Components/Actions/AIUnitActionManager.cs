using System;
using Redninja.Components.Decisions.AI;

namespace Redninja.Components.Actions
{
	[Serializable]
	public class AIUnitActionManager : UnitActionManager
	{
		public IAIBehavior AIBehavior { get; private set; }

		public void SetAIBehavior(AIRuleSet ruleSet, IBattleContext context)
		{
			if (ruleSet != null)
			{
				// this needs to be changed to accept IUnit instead of IUnitModel
				AIBehavior = new AIBehavior(context, Owner, ruleSet);
			}
			else
			{
				AIBehavior = null;
			}
		}
	}
}
