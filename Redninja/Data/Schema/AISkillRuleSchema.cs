using System;
using System.Collections.Generic;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class AISkillRuleSchema : IDataSource
	{
		public string DataId { get; set; }
		public int RefreshTime { get; set; }
		public int Weight { get; set; }

		public List<string> TriggerConditions { get; set; }
		public TargetTeam TargetType { get; set; }
		public List<string> TargetConditions { get; set; }
		public Dictionary<string, string> SkillPriorityMap { get; set; }
	}	

	
}
