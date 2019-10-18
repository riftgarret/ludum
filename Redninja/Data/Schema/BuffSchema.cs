using System;
using System.Collections.Generic;
using Redninja.Components.Buffs;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class BuffSchema
	{
		public string DataId { get; set; }
		public string Name { get; set; }
		public BuffConfig Config { get; set; }
		public string Executor { get; set; }
		public Dictionary<string, object> ExectorProps { get; set; }		
	}
}
