using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Skills;
using Redninja.System;

namespace Redninja.Data.Schema.Readers
{
	internal class ClassItemFactory : IDataItemFactory<IClassProvider>
	{
		public IClassProvider CreateInstance(string dataId, ISchemaStore store)
		{
			var cs = store.GetSchema<ClassSchema>(dataId);
			return ClassProvider.Build(b =>
				{
					b.SetAttackTime(cs.AttackTime[0], cs.AttackTime[1], cs.AttackTime[2]);
					foreach (ClassSkillSchema skill in cs.Skills.OrderBy(s => s.Level))
					{
						b.AddSkill(skill.Level, store.SingleInstance<ISkill>(skill.SkillId));
					}
					return b;
				});
		}
	}
}
