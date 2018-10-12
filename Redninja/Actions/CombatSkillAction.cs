using System.Collections.Generic;
using Redninja.Skills;

namespace Redninja.Actions
{
	public class CombatSkillAction : BattleActionBase
	{
		private readonly IBattleEntity entity;
		private readonly ICombatSkill skill;
		private readonly IList<SkillResolver> resolvers;

		public CombatSkillAction(IBattleEntity entity, ICombatSkill skill, IEnumerable<SkillResolver> resolvers)
			: base(skill.Time)
		{
			this.skill = skill;
			this.entity = entity;
			this.resolvers = new List<SkillResolver>(resolvers);
		}

		protected override void ExecuteAction(float timeDelta, float time)
		{
			foreach (SkillResolver r in resolvers)
			{
				if (!r.Resolved && PhaseProgress >= r.ExecutionStart)
				{
					SendBattleOperation(GetPhaseTimeAt(r.ExecutionStart), r.Resolve(entity, skill));
				}
			}
		}
	}
}
