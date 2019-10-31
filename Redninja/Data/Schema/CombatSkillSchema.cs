using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class CombatSkillSchema : IDataSource
	{
		public string DataId { get; set; }

		public string Name { get; set; }

		public List<float> Time { get; set; }

		public List<TargetingSetSchema> TargetingSets { get; set; }

		public Dictionary<string, int> DefaultStats { get; set; }		
	}

	[Serializable]
	internal class TargetingSetSchema
	{
		public string TargetingRuleId { get; set; }
		public List<BattleOperationSchema> Ops { get; set; }
	}

	[Serializable]
	internal class BattleOperationSchema
	{
		public float ExecutionStart { get; set; }
		public string OpType { get; set; }
		public JObject Params { get; set; }
		public string Pattern { get; set; }
		public Dictionary<string, int> Stats { get; set; }

	}	

	[Serializable]
	internal class BuffParameters
	{
		public string BufFId { get; set; }
		public bool IsHex { get; set; }
	}
}
