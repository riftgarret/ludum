using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions.AI
{
	public static class AIRuleFactory
	{		
		public static AISkillRule CreateAttackRule()
			=> new AISkillRule.Builder()
				.SetName("Attack")
				.SetRuleTargetType(TargetTeam.Enemy)
				.SetWeight(1)				
				.AddSkillAndPriority(null, AITargetPriorityFactory.NoPriority) // TODO
				.Build();
		
	}
}
