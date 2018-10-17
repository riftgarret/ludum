using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class EncounterSchema : IDataSource
	{
		public string DataId { get; set; }
		//public string SceneId { get; set; } // backgrounds, music, in fight dialog events
		public string EnemyGridSize { get; set; }
		public string PlayerGridSize { get; set; }
		public List<EncounterEnemy> Enemies { get; set; }
	}

	[Serializable]
	internal class EncounterEnemy
	{
		public string CharacterId { get; set; }
		public string AiBehaviorId { get; set; }
		public string Position { get; set; }				
	}
}
