using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions
{
	public abstract class TargetSpec : ITargetSpec
	{
		protected IBattleEntity source;		
		protected IBattleModel battleModel;

		public SkillTargetingSet TargetingSet { get; protected set; }

		public ITargetingRule TargetRule => TargetingSet.TargetingRule;

		public bool IsSelected => SelectedTarget != null;

		public ISelectedTarget SelectedTarget { get; protected set; }

		public TargetType TargetType => TargetRule.Type;

		public void Unselect() => SelectedTarget = null;

		public ISkill Skill { get; protected set; }

		public static ITargetSpec CreateSpec(SkillTargetingSet set, IBattleEntity source, ISkill skill, IBattleModel model)
		{
			TargetSpec spec;
			switch (set.TargetingRule.Type)
			{
				case TargetType.Entity:
					spec = new EntityTargetSpec();
					break;
				case TargetType.Pattern:
					spec = new PatternTargetSpec();
					break;
				default:
					throw new InvalidOperationException("Missing spec");
			}

			spec.source = source;
			spec.battleModel = model;
			spec.TargetingSet = set;
			spec.Skill = skill;

			return spec;
		}
	}
}
