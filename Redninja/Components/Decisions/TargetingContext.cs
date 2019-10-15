using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions
{
	
	internal class TargetingContext : ITargetingContext
	{				
		public ISkill Skill { get; }		

		public IBattleEntity Source { get; }

		public IReadOnlyList<ITargetSpec> TargetSpecs { get; private set; }

		public TargetingContext(
			IBattleEntity entity,
			ISkill skill,
			IBattleModel battleModel)
		{
			TargetSpecs = skill.Targets.Select(x => TargetSpec.CreateSpec(x, entity, skill, battleModel)).ToList();
			Skill = skill;
			Source = entity;
		}		

		public bool IsReady => TargetSpecs.All(x => x.IsSelected);

		public IBattleAction GetAction() => Skill.GetAction(Source, TargetSpecs.Select(x => x.SelectedTarget).ToList());
	}
}
