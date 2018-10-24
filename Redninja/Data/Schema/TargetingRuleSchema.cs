using System;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class TargetingRuleSchema : IDataSource
	{
		public string DataId { get; set; }
		public string Pattern { get; set; }
		public TargetTeam TargetTeam { get; set; }
		public TargetType TargetType { get; set; }
		public string TargetConditionName { get; set; }
	}
}
