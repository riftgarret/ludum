using System.Collections.Generic;
using Redninja.Skills;

namespace Redninja.Decisions
{
	/// <summary>
	/// Result for requesting what available skills a entity can use.
	/// </summary>
	public class SkillSelectionMeta
	{
		public IEnumerable<ICombatSkill> Skills { get; }
		public IBattleEntity Entity { get; }

		public SkillSelectionMeta(IBattleEntity entity, IEnumerable<ICombatSkill> skills)
		{
			Skills = skills;
			Entity = entity;
		}
	}
}
