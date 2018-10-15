using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.ConsoleDriver.Data.Schema
{
	[Serializable]
	public class SkillRootSchema
	{
		public List<CombatSkillSchema> CombatSkills { get; set; }

		public List<TargetingSetSchema> TargetSets { get; set; }
	}
}
