using Redninja.Skills;
using Redninja.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Decisions
{
	public class SkillTargetMeta
	{
		public IBattleEntity Entity { get; }
		public ICombatSkill Skill { get; }
		public TargetType TargetType => Skill.TargetRule.TargetType;

		private IBattleEntityManager entityManager;

		public SkillTargetMeta(
			IBattleEntity entity,
			ICombatSkill skill,
			IBattleEntityManager entityManager)
		{
			this.entityManager = entityManager;
			Skill = skill;
			Entity = entity;
		}

		/// <summary>
		/// Is this entity selectable?
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool IsValidTargetForRule(IBattleEntity entity)
			=> Skill.TargetRule.IsValidTarget(entity);

		public bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn)
			=> Skill.CombatRounds.FirstOrDefault(round => round.Pattern.IsInPattern(anchorRow, anchorColumn, targetRow, targetColumn)) != null;

	}
}
