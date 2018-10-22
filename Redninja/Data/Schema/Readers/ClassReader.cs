using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Skills;
using Redninja.System;

namespace Redninja.Data.Schema.Readers
{
	internal static class ClassReader
	{		
		public static void ReadRoot(List<ClassSchema> classes, IEditableDataStore<IClassProvider> store, IDataStore<ISkill> skillsStore)
		{
			foreach(ClassSchema cs in classes)
			{
				store[cs.DataId] = ClassProvider.Build(b =>
				{
					b.SetAttackTime(cs.AttackTime[0], cs.AttackTime[1], cs.AttackTime[2]);
					foreach (ClassSkillSchema skill in cs.Skills.OrderBy(s => s.Level))
					{
						b.AddSkill(skill.Level, skillsStore[skill.SkillId]);
					}
					return b;
				});
			}
		}
	}
}
