using System;
using System.Collections.Generic;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class SkillRootSchema
	{
		public List<CombatSkillSchema> CombatSkills { get; set; }

		public List<TargetingRuleSchema> TargetingRules { get; set; }

		public List<BuffSchema> Buffs { get; set; }
	}
}
