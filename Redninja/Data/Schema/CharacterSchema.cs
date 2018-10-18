using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class CharacterSchema : IDataSource
	{
		public string DataId { get; set; }
		public string Name { get; set; }
		public int Level { get; set; }
		public string Class { get; set; }
		public Dictionary<CombatStats, int> Stats { get; set; }		
		// equipment?
		// class?
		// level?
	}
}
