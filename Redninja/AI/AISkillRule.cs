﻿using Davfalcon.Revelator;
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
	public class AISkillRule : AIRuleBase
	{
		// targeting who gets focused should be uniform for rule
		private AITargetType TargetType { get; set; }
		private List<IAITargetCondition> FilterConditions { get; } = new List<IAITargetCondition>();
		private List<Tuple<IAITargetPriority, ICombatSkill>> SkillAssignments { get; } = new List<Tuple<IAITargetPriority, ICombatSkill>>();

		public override IBattleAction GenerateAction(IBattleEntity source, IBattleEntityManager bem)
		{
			SkillSelectionMeta skillMeta = DecisionHelper.GetAvailableSkills(source);

			// filter out what skills this rule uses
			IEnumerable<ICombatSkill> availableSkills = GetAssignableSkills(skillMeta);

			// attempt to find targets for first valid skill
			foreach (ICombatSkill skill in availableSkills)
			{
				// look for available targets
				SkillTargetMeta targetMeta = DecisionHelper.GetSelectableTargets(source, bem, skill);

				SelectedTarget target = TryFindTarget(targetMeta, source, bem);

				// found!				
				if (target != null)
				{
					return DecisionHelper.CreateAction(source, skill, target);
				}
			}
			return null;
		}

		internal IEnumerable<ICombatSkill> GetAssignableSkills(SkillSelectionMeta meta)
			=> meta.Skills.Intersect(SkillAssignments.Select(x => x.Item2));

		internal SelectedTarget TryFindTarget(SkillTargetMeta meta, IBattleEntity source, IBattleEntityManager bem)
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
			IEnumerable<IBattleEntity> leftoverTargets = AIHelper.FilterByType(TargetType, source, bem);

			// filter by skill rule
			leftoverTargets = leftoverTargets.Where(ex => skill.TargetRule.IsValidTarget(ex));

			// filter by filter conditions (exclude by finding first condition that fails)
			leftoverTargets = leftoverTargets.Where(ex => FilterConditions.First(cond => !cond.IsValid(ex)) == null);
			return leftoverTargets;
		}		

		/// <summary>
		/// Builder class for a rule.
		/// </summary>
		public class Builder : AIRuleBase.BuilderBase<Builder>, IBuilder<AISkillRule>
		{
			private AISkillRule rule;
			private AITargetType? nullableType;

			public Builder() => Reset();

			public Builder Reset()
			{
				rule = new AISkillRule();
				nullableType = null;
				ResetBase(rule);
				return this;
			}

			/// <summary>
			/// Target type represents which group will be targeted with the skill.
			/// </summary>
			/// <param name="type"></param>
			/// <returns></returns>
			public Builder SetRuleTargetType(AITargetType type)
			{
				nullableType = type;
				return this;
			}

			/// <summary>
			/// Filter conditions will filter out entities from the original target group.
			/// </summary>
			/// <param name="condition"></param>
			/// <returns></returns>
			public Builder AddFilterCondition(IAITargetCondition condition)
			{
				rule.FilterConditions.Add(condition);
				return this;
			}

			/// <summary>
			/// Skill to use and priority.
			/// </summary>
			/// <param name="skill"></param>
			/// <param name="priority"></param>
			/// <returns></returns>
			public Builder AddSkillAndPriority(ICombatSkill skill, IAITargetPriority priority)
			{
				rule.SkillAssignments.Add(Tuple.Create(priority, skill));
				return this;
			}

			public AISkillRule Build()
			{
				// validation check				
				if (rule.SkillAssignments.Count() == 0) throw new InvalidOperationException($"Missing at least 1 skill assignment for Rule: {rule.RuleName}");
				if (nullableType == null) throw new InvalidOperationException($"Missing AITargetType for Rule: {rule.RuleName}");
				BuildBase();

				rule.TargetType = nullableType.Value;
				
				AISkillRule builtRule = this.rule;
				Reset();
				return builtRule;
			}
		}
	}
}
