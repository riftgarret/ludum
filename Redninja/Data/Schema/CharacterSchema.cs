using System;
using System.Collections.Generic;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class CharacterSchema : IDataSource
	{
		public string DataId { get; set; }
		public string Name { get; set; }
		public int Level { get; set; }
		public string Class { get; set; }
		public Dictionary<Stat, int> Stats { get; set; }		
		// equipment?
		// class?
		// level?
	}
}
