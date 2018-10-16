using System;
using System.Collections.Generic;
using Davfalcon.Revelator.Borger;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class CombatSkillSchema : IDataSource
	{
		public string DataId { get; set; }
		
		public string Name { get; set; }

		public List<float> Time { get; set; }

		public List<string> TargetSetIds { get; set; }

		public int BaseDamage { get; set; }

		public int CritMultiplier { get; set; }

		public CombatStats BonusDamageStat { get; set; }

		public List<DamageType> DamageTypes { get; set; }
	}	
}
