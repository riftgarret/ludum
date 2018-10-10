using Redninja.Skills;
using Redninja.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Actions
{
	public class CombatSkillAction : BattleActionBase
	{
		private readonly IBattleEntity entity;
		private readonly ICombatSkill skill;
		private readonly IEnumerable<SkillResolver> resolvers;

		public CombatSkillAction(IBattleEntity entity, ICombatSkill skill, IEnumerable<SkillResolver> resolvers)
			: base(skill.Time)
		{
			this.skill = skill;
			this.entity = entity;
			this.resolvers = resolvers;
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
