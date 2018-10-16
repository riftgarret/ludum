using System;
using System.Collections.Generic;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class SkillRootSchema
	{
		public List<CombatSkillSchema> CombatSkills { get; set; }

		public List<TargetingSetSchema> TargetSets { get; set; }
	}
}
