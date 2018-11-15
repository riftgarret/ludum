using System;
using System.Collections.Generic;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class AIRulesRootSchema
	{
		public List<AISkillRuleSchema> SkillRules { get; set; }
		public List<AIAttackRuleSchema> AttackRules { get; set; }
	}
}
