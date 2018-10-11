using Davfalcon.Revelator;
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
	public class AISkillRule : IAIRule
	{
		// trigger conditions can rely on different targets
		private List<Tuple<AITargetType, IAITargetCondition>> TriggerConditions { get; } = new List<Tuple<AITargetType, IAITargetCondition>>();

		// targeting who gets focused should be uniform for rule
		private AITargetType TargetType { get; set; }
		private List<IAITargetCondition> FilterConditions { get; } = new List<IAITargetCondition>();
		private List<Tuple<IAITargetPriority, ICombatSkill>> SkillAssignments { get; } = new List<Tuple<IAITargetPriority, ICombatSkill>>();

		public string RuleName { get; private set; } = "Unnamed Rule";

		public int Weight { get; private set; }

		public int RefreshTime { get; private set; }


		public bool IsValidTriggerConditions(IBattleEntity source, IBattleEntityManager entityManager)
		{
			// find first fail, if no failed, then true
			return TriggerConditions.First(trigger =>
				AIHelper.FilterByType(trigger.Item1, source, entityManager)
				.First(ex => !trigger.Item2.IsValid(ex)) != null) == null;
		}

		public IBattleAction GenerateAction(IBattleEntity source, IBattleEntityManager bem)
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
		public class Builder : IBuilder<AISkillRule>
		{
			private AISkillRule rule;
			private AITargetType? nullableType;

			public Builder() => Reset();

			public Builder Reset()
			{
				rule = new AISkillRule();
				nullableType = null;
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
			/// Trigger conditions pairs can be any target meating a condition.
			/// </summary>
			/// <param name="type"></param>
			/// <param name="condition"></param>
			/// <returns></returns>
			public Builder AddTriggerCondition(AITargetType type, IAITargetCondition condition)
			{
				rule.TriggerConditions.Add(Tuple.Create(type, condition));
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

			/// <summary>
			/// Sets the name, mostly useful for debugging purposes.
			/// </summary>
			/// <param name="name"></param>
			/// <returns></returns>
			public Builder SetName(string name)
			{
				rule.RuleName = name;
				return this;
			}

			/// <summary>
			/// Weight that represents the chance in a sum of weights to be used
			/// </summary>
			/// <param name="weight"></param>
			/// <returns></returns>
			public Builder SetWeight(int weight)
			{
				rule.Weight = weight;
				return this;
			}

			public AISkillRule Build()
			{
				// validation check
				if (rule.SkillAssignments.Count() == 0) throw new InvalidOperationException($"Missing at least 1 skill assignment for Rule: {rule.RuleName}");
				if (nullableType == null) throw new InvalidOperationException($"Missing AITargetType for Rule: {rule.RuleName}");				
				if (rule.Weight <= 0) throw new InvalidOperationException($"Invalid weight, must be > 0 for Rule: {rule.RuleName}");

				if (rule.TriggerConditions.Count() == 0)
				{
					Logging.RLog.D(this, $"No conditions found for {rule.RuleName}, adding always true");
					AddTriggerCondition(AITargetType.Self, AIConditionFactory.AlwaysTrue);
				}

				rule.TargetType = nullableType.Value;

				AISkillRule builtRule = this.rule;
				Reset();
				return builtRule;
			}
		}
	}
}
