using Redninja.Decisions;
using Redninja.Skills;
using Redninja.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.AI
{
	/// <summary>
	/// This represents a set of conditions that may be attached to a list of
	/// actionable items. All skills in this rule should be unique.
	/// </summary>
	public class AISkillRule : AIRuleBase
	{
		// targeting who gets focused should be uniform for rule
		private TargetTeam TargetType { get; set; }
		private List<IAITargetCondition> FilterConditions { get; } = new List<IAITargetCondition>();
		private List<Tuple<IAITargetPriority, ISkill>> SkillAssignments { get; } = new List<Tuple<IAITargetPriority, ISkill>>();

		public override IBattleAction GenerateAction(IBattleEntity source, IDecisionHelper decisionHelper)
		{
			IActionPhaseHelper skillMeta = decisionHelper.GetAvailableSkills(source);

			// filter out what skills this rule uses
			IEnumerable<ISkill> availableSkills = GetAssignableSkills(skillMeta);

			// attempt to find targets for first valid skill
			foreach (ISkill skill in availableSkills)
			{
				// look for available targets
				ITargetPhaseHelper targetMeta = decisionHelper.GetTargetingManager(source, skill);

				// found!				
				while (TryFindTarget(targetMeta, source, decisionHelper.BattleEntityManager, out ISelectedTarget selectedTarget))
				{
					targetMeta.SelectTarget(selectedTarget);

					if (targetMeta.Ready)
					{
						return targetMeta.GetAction();
					}
				}
			}
			return null;
		}

		internal IEnumerable<ISkill> GetAssignableSkills(IActionPhaseHelper meta)
			=> meta.Skills.Intersect(SkillAssignments.Select(x => x.Item2));

		internal bool TryFindTarget(ISkillTargetingInfo meta, IBattleEntity source, IBattleEntityManager bem, out ISelectedTarget selectedTarget)
		{
			// filter targets
			IEnumerable<IBattleEntity> filteredTargets = FilterTargets(meta.TargetingRule, source, bem);

			if (filteredTargets.Count() == 0)
			{
				selectedTarget = null;
				return false; // didnt find any valid targets
			}

			// select best target
			IAITargetPriority targetPriority = SkillAssignments.FirstOrDefault(x => x.Item2 == meta.Skill).Item1;
			IBattleEntity entityTarget = targetPriority.GetBestTarget(filteredTargets);

			// convert into ISelectedTarget
			selectedTarget = meta.GetSelectedTarget(entityTarget);
			return true;
		}

		internal IEnumerable<IBattleEntity> FilterTargets(ITargetingRule targetingRule, IBattleEntity source, IBattleEntityManager bem)
		{
			// first filter by TargetType
			IEnumerable<IBattleEntity> leftoverTargets = AIHelper.FilterByType(TargetType, source, bem);

			// filter by skill rule
			leftoverTargets = leftoverTargets.Where(ex => targetingRule.IsValidTarget(ex, source));

			// filter by filter conditions (exclude by finding first condition that fails)
			leftoverTargets = leftoverTargets.Where(ex => FilterConditions.FirstOrDefault(cond => !cond.IsValid(ex)) == null);
			return leftoverTargets;
		}		

		/// <summary>
		/// Builder class for a rule.
		/// </summary>
		public class Builder : BuilderBase<Builder, AISkillRule>
		{
			private AISkillRule rule;
			private TargetTeam? nullableType;

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
			public Builder SetRuleTargetType(TargetTeam type)
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
			public Builder AddSkillAndPriority(ISkill skill, IAITargetPriority priority)
			{
				rule.SkillAssignments.Add(Tuple.Create(priority, skill));
				return this;
			}

			public override AISkillRule Build()
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
