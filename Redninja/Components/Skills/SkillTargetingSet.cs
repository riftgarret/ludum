using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	public class SkillTargetingSet
	{
		public ITargetingRule TargetingRule { get; private set; }
		public IList<IBattleOperationDefinition> OpDefinitions { get; } = new List<IBattleOperationDefinition>();

		public SkillTargetingSet(ITargetingRule targetingRule)
		{
			TargetingRule = targetingRule;
		}
	}
}
