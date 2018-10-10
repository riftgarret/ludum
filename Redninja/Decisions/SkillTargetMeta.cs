using System.Collections.Generic;
using System.Linq;
using Redninja.Actions;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.Decisions
{
	public class SkillTargetMeta
	{
		private readonly IBattleEntityManager entityManager;
		private readonly IEnumerable<SkillResolver>[] skillResolvers;
		private int currentIndex = 0;

		public IBattleEntity Entity { get; }
		public ICombatSkill Skill { get; }
		public SkillTargetingSet TargetingSet => Skill.Targets[currentIndex];
		public ITargetingRule TargetingRule => TargetingSet.TargetingRule;
		public TargetType TargetType => TargetingRule.Type;
		public bool Ready => currentIndex > Skill.Targets.Count;

		public SkillTargetMeta(
			IBattleEntity entity,
			ICombatSkill skill,
			IBattleEntityManager entityManager)
		{
			this.entityManager = entityManager;
			Skill = skill;
			Entity = entity;

			skillResolvers = new IEnumerable<SkillResolver>[Skill.Targets.Count];
		}

		/// <summary>
		/// Checks if the entity is targetable.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool IsValidTargetForRule(IBattleEntity entity)
			=> TargetingRule.IsValidTarget(entity);

		/// <summary>
		/// Checks if the location is in the target pattern.
		/// </summary>
		/// <param name="anchorRow"></param>
		/// <param name="anchorColumn"></param>
		/// <param name="targetRow"></param>
		/// <param name="targetColumn"></param>
		/// <returns></returns>
		public bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn)
			=> TargetingRule.Pattern?.ContainsLocation(anchorRow, anchorColumn, targetRow, targetColumn) ?? false;

		/// <summary>
		/// Selects a target and moves on to the next target.
		/// </summary>
		/// <param name="target"></param>
		public void SelectTarget(IBattleEntity target) {
			skillResolvers[currentIndex] = TargetingSet.GetSkillResolvers(new SelectedTarget(TargetingRule, target));
			Next();
		}

		/// <summary>
		/// Selects a pattern anchor to target and moves on to the next target.
		/// </summary>
		/// <param name="anchor"></param>
		public void SelectTarget(int team, Coordinate anchor)
		{
			skillResolvers[currentIndex] = TargetingSet.GetSkillResolvers(team, anchor);
			Next();
		}

		private void Next()
		{
			currentIndex++;
		}

		public void Back()
		{
			currentIndex--;
			skillResolvers[currentIndex] = null;
		}

		public IBattleAction BuildAction()
			=> new CombatSkillAction(Entity, Skill,
				skillResolvers.Aggregate(new List<SkillResolver>() as IEnumerable<SkillResolver>, (accumulator, next) => accumulator.Union(next)));
	}
}
