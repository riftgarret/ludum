using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.AI
{
	public class AIRuleFactory
	{
		private AIRuleFactory() { }

		public static AISkillRule CreateAttackRule()
			=> new AISkillRule.Builder()
				.SetName("Attack")
				.SetRuleTargetType(AITargetType.Enemy)
				.SetWeight(1)				
				.AddSkillAndPriority(null, AITargetPriorityFactory.NoPriority) // TODO
				.Build();
		
	}
}
