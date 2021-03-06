﻿using System;
using System.Collections.Generic;
using Redninja.Components.Buffs;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class BuffSchema : IDataSource
	{
		public string DataId { get; set; }
		public string Name { get; set; }
		public BuffProperties Properties { get; set; }
		public string Executor { get; set; }
		public Dictionary<string, object> ExectorProps { get; set; }					
	}
}
