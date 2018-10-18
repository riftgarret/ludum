using System;
using System.Collections.Generic;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class AIRuleSetSchema : IDataSource
	{
		public string DataId { get; set; }
		public List<string> RuleIds { get; set; }
	}
}
