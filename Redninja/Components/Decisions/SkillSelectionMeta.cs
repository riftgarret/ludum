using System.Collections.Generic;
using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	/// <summary>
	/// Result for requesting what available skills a entity can use.
	/// </summary>
	public class SkillSelectionMeta : IActionPhaseHelper
	{
		public IEnumerable<ISkill> Skills { get; }
		public IBattleEntity Entity { get; }

		public SkillSelectionMeta(IBattleEntity entity, IEnumerable<ISkill> skills)
		{
			Skills = skills;
			Entity = entity;
		}
	}
}
