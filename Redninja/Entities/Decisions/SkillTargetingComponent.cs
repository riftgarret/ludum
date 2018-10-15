using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.View;

namespace Redninja.Entities.Decisions
{
	internal class SkillTargetingComponent : ITargetingComponent
	{
		private readonly IBattleEntityManager entityManager;
		private readonly ISelectedTarget[] selectedTargets;
		private int currentIndex = 0;

		public IEntityModel Entity { get; }
		IEntityModel ITargetingView.Entity => Entity;
		public ISkill Skill { get; }
		public SkillTargetingSet TargetingSet => Skill.Targets[currentIndex];
		public ITargetingRule TargetingRule => TargetingSet.TargetingRule;
		public TargetType TargetType => TargetingRule.Type;
		public bool Ready => currentIndex >= Skill.Targets.Count;

		public SkillTargetingComponent(
			IEntityModel entity,
			ISkill skill,
			IBattleEntityManager entityManager)
		{
			this.entityManager = entityManager;
			Skill = skill;
			Entity = entity;

			selectedTargets = new ISelectedTarget[Skill.Targets.Count];
		}

		public IEnumerable<IEntityModel> GetTargetableEntities()
		{
			switch (TargetingRule.Team)
			{
				case TargetTeam.Ally:
					return entityManager.Entities.Where(e => e.Team == Entity.Team && IsValidTarget(e));
				case TargetTeam.Enemy:
					return entityManager.Entities.Where(e => e.Team != Entity.Team && IsValidTarget(e));
				case TargetTeam.Self:
					return IsValidTarget(Entity) ? new List<IEntityModel>() { Entity }.AsReadOnly() as IEnumerable<IEntityModel> : new EmptyEnumerable<IEntityModel>();
				default: throw new InvalidOperationException();
			}
		}

		IEnumerable<IEntityModel> ITargetingView.GetTargetableEntities() => GetTargetableEntities();

		/// <summary>
		/// Checks if the entity is targetable.
		/// </summary>
		/// <param name="targetEntity"></param>
		/// <returns></returns>
		public bool IsValidTarget(IEntityModel targetEntity)
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
		public ISelectedTarget GetSelectedTarget(IEntityModel target)
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
			selectedTargets[currentIndex] = target;
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
				selectedTargets[currentIndex] = null;
				return true;
			}
			else return false;
		}

		public IBattleAction GetAction()
			=> Skill.GetAction(Entity, selectedTargets);
	}
}
