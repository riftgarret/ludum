using System;
using System.Collections.Generic;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class AIAttackRuleSchema : IDataSource
	{
		public string DataId { get; set; }
		public int RefreshTime { get; set; }
		public int Weight { get; set; }
		public string Priority { get; set; }

		public Dictionary<TargetTeam, List<string>> TriggerConditions { get; set; }		
	}	

	
}
