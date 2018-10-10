using Redninja.Decisions;
using Redninja.Skills;
using Redninja.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.AI
{
	/// <summary>
	/// This represents a set of conditions that may be attached to a list of
	/// actionable items. All skills in this rule should be unique.
	/// </summary>
	public class AIRule
	{
		// trigger conditions can rely on different targets
		private IEnumerable<Tuple<AITargetType, IAITargetCondition>> TriggerConditions { get; }

		// targeting who gets focused should be uniform for rule
		private AITargetType TargetType { get; }
		private IEnumerable<IAITargetCondition> FilterConditions { get; }
		private IEnumerable<Tuple<IAITargetPriority, ICombatSkill>> SkillAssignments { get; }

		public int Weight { get; }

		public int RefreshTime { get; }


		public bool IsValidTriggerConditions(IBattleEntity source, IBattleEntityManager entityManager)
		{
			// find first fail, if no failed, then true
			return TriggerConditions.First(trigger =>
				FilterByType(trigger.Item1, source, entityManager)
				.First(ex => !trigger.Item2.IsValid(ex)) != null) == null;
		}

		public IEnumerable<ICombatSkill> GetAssignableSkills(SkillSelectionMeta meta)
			=> meta.Skills.Intersect(SkillAssignments.Select(x => x.Item2));

		public SelectedTarget TryFindTarget(SkillTargetMeta meta, IBattleEntity source, IBattleEntityManager bem)
		{
			// filter targets
			IEnumerable<IBattleEntity> filteredTargets = FilterTargets(meta.Skill, source, bem);

			if (filteredTargets.Count() == 0)
			{
				return null; // didnt find any valid targets
			}

			// select best target
			IAITargetPriority targetPriority = SkillAssignments.First(x => x.Item2 == meta.Skill).Item1;
			IBattleEntity entityTarget = targetPriority.GetBestTarget(filteredTargets);

			// convert into SelectedTarget
			return new SelectedTarget(entityTarget);
		}

		private IEnumerable<IBattleEntity> FilterTargets(ICombatSkill skill, IBattleEntity source, IBattleEntityManager bem)
		{
			// first filter by TargetType
			IEnumerable<IBattleEntity> leftoverTargets = FilterByType(TargetType, source, bem);

			// filter by skill rule
			leftoverTargets = leftoverTargets.Where(ex => skill.TargetRule.IsValidTarget(ex));

			// filter by filter conditions (exclude by finding first condition that fails)
			leftoverTargets = leftoverTargets.Where(ex => FilterConditions.First(cond => !cond.IsValid(ex)) == null);
			return leftoverTargets;
		}

		private IEnumerable<IBattleEntity> FilterByType(AITargetType type, IBattleEntity source, IBattleEntityManager bem)
		{
			switch (type)
			{
				case AITargetType.Ally:
					return bem.AllEntities.Where(x => x.Team == source.Team);
				case AITargetType.Enemy:
					return bem.AllEntities.Where(x => x.Team != source.Team);
				case AITargetType.Self:
					return new List<IBattleEntity>() { source };
				default:
					throw new InvalidProgramException("Unexpected target type, should implement!");
			}
		}
	}
}
