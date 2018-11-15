using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions.AI
{
	internal static class AIRuleFactory
	{		
		public static AIAttackRule CreateDefaultAttackRule()
			=> new AIAttackRule.Builder()				
				.SetName("Attack")			
				.SetWeight(1)				
				.SetTargetPriority(AITargetPriorityFactory.Any) 
				.Build();		
	}
}
