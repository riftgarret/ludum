using System.Collections.Generic;
using System.Linq;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	/// <summary>
	/// Result for requesting what available skills a entity can use.
	/// </summary>
	internal class SkillSelectionContext : IActionContext
	{
		public IBattleEntity Entity { get; }		
		public IEnumerable<ISkill> Skills { get; }

		public SkillSelectionContext(IBattleEntity entity, ISkillProvider skillProvider)
		{
			Entity = entity;			
			Skills = skillProvider.GetSkills();
		}
	}
}
