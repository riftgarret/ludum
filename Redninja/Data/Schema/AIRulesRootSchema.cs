using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class AIRulesRootSchema
	{
		public List<AISkillRuleSchema> SkillRules { get; set; }
	}
}
