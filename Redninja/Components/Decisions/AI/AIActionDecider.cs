using System;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions.AI
{
	internal class AIActionDecider : IActionDecider
	{
		private readonly AIBehavior behavior;		
		private readonly IDecisionHelper decisionHelper;
		private AIExecutor aiExecutor; // lazy init

		public AIActionDecider(AIBehavior behavior, IDecisionHelper decisionHelper)
		{
			this.decisionHelper = decisionHelper;
			this.behavior = behavior;				
		}

		public event Action<IUnitModel, IBattleAction> ActionSelected;

		public void ProcessNextAction(IUnitModel source, IBattleModel battleModel)
		{
			if (aiExecutor == null) aiExecutor = new AIExecutor(source, behavior, decisionHelper, new AIRuleTracker(battleModel));

			IBattleAction action = aiExecutor.ResolveAction();
			ActionSelected?.Invoke(source, action);
		}
	}
}
