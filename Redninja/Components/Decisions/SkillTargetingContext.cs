using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions
{
	internal class SkillTargetingContext : ITargetingContext
	{
		private readonly IBattleModel battleModel;
		private readonly ISelectedTarget[] selectedTargets;
		private int currentIndex = 0;

		public IUnitModel Entity { get; }
		public ISkill Skill { get; }
		public SkillTargetingSet TargetingSet => Skill.Targets[currentIndex];
		public ITargetingRule TargetingRule => TargetingSet.TargetingRule;
		public TargetType TargetType => TargetingRule.Type;
		public bool Ready => currentIndex >= Skill.Targets.Count;

		public SkillTargetingContext(
			IUnitModel entity,
			ISkill skill,
			IBattleModel battleModel)
		{
			this.battleModel = battleModel;
			Skill = skill;
			Entity = entity;

			selectedTargets = new ISelectedTarget[Skill.Targets.Count];
		}

		public IEnumerable<IUnitModel> GetTargetableEntities()
		{
			switch (TargetingRule.Team)
			{
				case TargetTeam.Ally:
					return battleModel.Entities.Where(e => e.Team == Entity.Team && IsValidTarget(e));
				case TargetTeam.Enemy:
					return battleModel.Entities.Where(e => e.Team != Entity.Team && IsValidTarget(e));
				case TargetTeam.Self:
					return IsValidTarget(Entity) ? Enumerable.Repeat(Entity, 1) : Enumerable.Empty<IUnitModel>();
				default: throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Checks if the entity is targetable.
		/// </summary>
		/// <param name="targetEntity"></param>
		/// <returns></returns>
		public bool IsValidTarget(IUnitModel targetEntity)
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
		public ISelectedTarget GetSelectedTarget(IUnitModel target)
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
