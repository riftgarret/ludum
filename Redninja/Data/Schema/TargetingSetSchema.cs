using Redninja.Components.Targeting;
using System;
using System.Collections.Generic;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class TargetingSetSchema : IDataSource
	{
		public string DataId { get; set; }
		//public string PatternId { get; set; }
		public TargetTeam TargetTeam { get; set; }
		public TargetType TargetType { get; set; }
		public TargetConditionEnum TargetConditionEnum { get; set; }
		public List<CombatRoundSchema> CombatRounds { get; set; }
	}

	[Serializable]
	internal class CombatRoundSchema
	{
		public float ExecutionStart { get; set; }
		public OperationEnum OperationEnum { get; set; }
		public string Pattern { get; set; }
	}

	//public class TargetRuleSchema
	//{
	//	public string PatternId { get; set; }
	//	public TargetType TargetType { get; set; }
	//	public string TargetConditionId { get; set; }
	//}	
}
