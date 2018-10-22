using System;
using System.Collections.Generic;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class ClassSchema
	{
		public string DataId { get; set; }
		public string Name { get; set; }
		public List<float> AttackTime { get; set; }
		public List<ClassSkillSchema> Skills { get; set; }
	}
}
