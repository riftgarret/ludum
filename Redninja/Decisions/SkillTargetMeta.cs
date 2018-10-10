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
		IBattleEntity Entity { get; }
		ICombatSkill Skill { get; }
		TargetType TargetType => Skill.TargetRule.Type;

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

		///// <summary>
		///// Is this entity selectable?
		///// </summary>
		///// <param name="entity"></param>
		///// <returns></returns>
		//public bool IsValidTargetForRule(IBattleEntity entity)
		//	=> Skill.TargetRule.IsValidTarget(entity);

		//public bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn)
		//	=> Skill.CombatRounds.First(round => round.Pattern.ContainsLocation(anchorRow, anchorColumn, targetRow, targetColumn)) != null;

		///// <summary>
		///// Select target and return evaluated Battle Action
		///// </summary>
		///// <param name="target"></param>
		///// <returns></returns>
		//public SelectedTargets CreateSelectedTarget(IBattleEntity target)
		//	=> CreateSelectTarget(target, 0, 0, 0);

		///// <summary>
		///// Select target and return evaluated Battle Action
		///// </summary>
		///// <param name="target"></param>
		///// <returns></returns>
		//public SelectedTargets CreateSelectTarget(int anchorRow, int anchorColumn, int team)
		//	=> CreateSelectTarget(null, anchorRow, anchorColumn, team);

		//private SelectedTargets CreateSelectTarget(IBattleEntity entity, int anchorRow, int anchorColumn, int team)
		//	=> new SelectedTargets(entity, anchorRow, anchorColumn, team);
	}
}
