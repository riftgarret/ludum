using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Collections.Generic;
using Redninja.Actions;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.Decisions
{
	public class SkillTargetMeta : ITargetPhaseHelper
	{
		private readonly IBattleEntityManager entityManager;
		private readonly IEnumerable<SkillResolver>[] skillResolvers;
		private int currentIndex = 0;

		public IBattleEntity Entity { get; }
		public ICombatSkill Skill { get; }
		public SkillTargetingSet TargetingSet => Skill.Targets[currentIndex];
		public ITargetingRule TargetingRule => TargetingSet.TargetingRule;
		public TargetType TargetType => TargetingRule.Type;
		public bool Ready => currentIndex >= Skill.Targets.Count;

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

		public IEnumerable<IBattleEntity> GetTargetableEntities()
		{
			switch (TargetingRule.Team)
			{
				case TargetTeam.Ally:
					return entityManager.AllEntities.Where(e => e.Team == Entity.Team && IsValidTarget(e));
				case TargetTeam.Enemy:
					return entityManager.AllEntities.Where(e => e.Team != Entity.Team && IsValidTarget(e));
				case TargetTeam.Self:
					return IsValidTarget(Entity) ? new List<IBattleEntity>() { Entity }.AsReadOnly() as IEnumerable<IBattleEntity> : new EmptyEnumerable<IBattleEntity>();
				default: throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Checks if the entity is targetable.
		/// </summary>
		/// <param name="targetEntity"></param>
		/// <returns></returns>
		public bool IsValidTarget(IBattleEntity targetEntity)
			=> TargetingRule.IsValidTarget(Entity, targetEntity);

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
		/// Selects a target entity.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public ISelectedTarget GetSelectedTarget(IBattleEntity target)
			=> new SelectedTarget(TargetingRule, target);

		/// <summary>
		/// Selects a target anchor for the pattern.
		/// </summary>
		/// <param name="team"></param>
		/// <param name="anchor"></param>
		/// <returns></returns>
		public ISelectedTarget GetSelectedTarget(int team, Coordinate anchor)
			=> new SelectedTargetPattern(TargetingRule, TargetingRule.Pattern, team, anchor);

		/// <summary>
		/// Selects a target and moves on to the next target.
		/// </summary>
		/// <param name="target"></param>
		public void SelectTarget(ISelectedTarget target) {
			skillResolvers[currentIndex] = TargetingSet.GetSkillResolvers(target);
			Next();
		}

		private void Next()
		{
			currentIndex++;
		}

		public bool Back()
		{
			if (currentIndex > 0)
			{
				currentIndex--;
				skillResolvers[currentIndex] = null;
				return true;
			}
			else return false;
		}

		public IBattleAction GetAction()
			=> new CombatSkillAction(Entity, Skill,
				skillResolvers.Aggregate(new List<SkillResolver>() as IEnumerable<SkillResolver>, (accumulator, next) => accumulator.Union(next)));
	}
}
