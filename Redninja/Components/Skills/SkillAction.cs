using System.Collections.Generic;
using Redninja.Components.Actions;

namespace Redninja.Components.Skills
{
	internal class SkillAction : BattleActionBase
	{
		private readonly IBattleEntity entity;
		private readonly ISkill skill;
		private readonly IEnumerable<ISkillResolver> resolvers;

		public SkillAction(IBattleEntity entity, ISkill skill, IEnumerable<ISkillResolver> resolvers)
			: base(skill.Name, skill.Time)
		{
			this.skill = skill;
			this.entity = entity;
			this.resolvers = resolvers;
		}

		protected override void ExecuteAction(float timeDelta, float time)
		{
			foreach (ISkillResolver r in resolvers)
			{
				if (!r.Resolved && PhaseProgress >= r.ExecutionStart)
				{
					SendBattleOperation(GetPhaseTimeAt(r.ExecutionStart), r.Resolve(entity));
				}
			}
		}
	}
}
