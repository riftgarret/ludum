using System;
using System.Collections.Generic;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class ClassSchema : IDataSource
	{
		public string DataId { get; set; }
		public string Name { get; set; }
		public List<float> AttackTime { get; set; }
		//public List<WeaponType> AttackWeapons { get; set; }
		public List<ClassSkillSchema> Skills { get; set; }
	}
}
