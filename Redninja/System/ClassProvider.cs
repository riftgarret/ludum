using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Redninja.Components.Actions;
using Redninja.Components.Skills;

namespace Redninja.System
{
	internal class ClassProvider : IClassProvider
	{
		private ActionTime attackTime;
		private readonly List<SkillLevel> skills = new List<SkillLevel>();

		private struct SkillLevel
		{
			public int level;
			public ISkill skill;

			public SkillLevel(int level, ISkill skill)
			{
				this.level = level;
				this.skill = skill;
			}
		}

		private class SkillProvider : ISkillProvider
		{
			private readonly ClassProvider c;
			private readonly int level;

			public SkillProvider(ClassProvider c, int level)
			{
				this.c = c;
				this.level = level;
			}

			public IEnumerable<ISkill> GetSkills()
				=> c.skills.Where(s => s.level <= level).Select(s => s.skill);
		}

		public ISkillProvider GetSkillProvider(int level)
			=> new SkillProvider(this, level);

		private ClassProvider() { }

		public static IClassProvider Build(Func<Builder, IBuilder<IClassProvider>> func)
			=> func(new Builder()).Build();

		public class Builder : BuilderBase<ClassProvider, IClassProvider, Builder>
		{
			public Builder() => Reset();
			public override Builder Reset() => Reset(new ClassProvider());
			public Builder SetAttackTime(float prepare, float execute, float recover) => Self(c => c.attackTime = new ActionTime(prepare, execute, recover));
			public Builder AddSkill(int level, ISkill skill) => Self(c => c.skills.Add(new SkillLevel(level, skill)));
		}
	}
}
