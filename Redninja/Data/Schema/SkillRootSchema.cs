using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class SkillRootSchema
	{
		public List<CombatSkillSchema> CombatSkills { get; set; }

		public List<TargetingSetSchema> TargetSets { get; set; }
	}
}
