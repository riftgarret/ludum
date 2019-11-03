using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Combat;
using Redninja.Components.Skills;
using Redninja.Logging;

namespace Redninja.Components.Actions
{
	/// <summary>
	/// Skill wrapper around Battle Operation
	/// </summary>
	internal class SkillAction : BattleOperationAction
	{		
		private readonly ISkill skill;		

		public SkillAction(IBattleEntity entity, ISkill skill, IEnumerable<IBattleOperation> operations)
			: base(skill.Name, skill.Time, operations)
		{
			this.skill = skill;
			RLog.D(this, $"Created new SkillAction: [{skill.Name}, {entity.Name}, Ops = {operations.Count()}]");
		}
	}
}
