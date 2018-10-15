using System.Collections.Generic;
using Redninja.Components.Actions;

namespace Redninja.Components.Skills
{
	public class SkillAction : BattleActionBase
	{
		private readonly IEntityModel entity;
		private readonly ISkill skill;
		private readonly IList<ISkillResolver> resolvers;

		public SkillAction(IEntityModel entity, ISkill skill, IEnumerable<ISkillResolver> resolvers)
			: base(skill.Time)
		{
			this.skill = skill;
			this.entity = entity;
			this.resolvers = new List<ISkillResolver>(resolvers);
		}

		protected override void ExecuteAction(float timeDelta, float time)
		{
			foreach (ISkillResolver r in resolvers)
			{
				if (!r.Resolved && PhaseProgress >= r.ExecutionStart)
				{
					SendBattleOperation(GetPhaseTimeAt(r.ExecutionStart), r.Resolve(entity, skill));
				}
			}
		}
	}
}
