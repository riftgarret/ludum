using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Combat;

namespace Redninja.Components.Skills
{
	internal class SkillAction : BattleActionBase
	{
		private readonly IBattleEntity entity;
		private readonly ISkill skill;
		private readonly IEnumerable<IBattleOperation> operations;

		public SkillAction(IBattleEntity entity, ISkill skill, IEnumerable<IBattleOperation> operations)
			: base(skill.Name, skill.Time)
		{
			this.skill = skill;
			this.entity = entity;
			this.operations = operations;
		}

		protected override void ExecuteAction(float timeDelta, float time)
		{
			foreach (IBattleOperation op in operations)
			{
				if (!op.Executed && PhaseProgress >= op.ExecutionStart)
				{
					SendBattleOperation(GetPhaseTimeAt(op.ExecutionStart), op);					
				}
			}
		}
	}
}
