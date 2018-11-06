using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions.AI
{
	/// <summary>
	/// This represents a set of conditions that may be attached to a list of
	/// actionable items. All skills in this rule should be unique.
	/// </summary>
	public class AISkillRule : AIRuleBase, IAISkillRule
	{
		// targeting who gets focused should be uniform for rule
		public TargetTeam TargetType { get; private set; }
		private List<IAITargetCondition> filterConditions = new List<IAITargetCondition>();
		public IEnumerable<IAITargetCondition> FilterConditions => filterConditions;
		private List<Tuple<IAITargetPriority, ISkill>> skillAssignments = new List<Tuple<IAITargetPriority, ISkill>>();
		public IEnumerable<Tuple<IAITargetPriority, ISkill>> SkillAssignments => skillAssignments;

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
				rule.filterConditions.Add(condition);
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
				rule.skillAssignments.Add(Tuple.Create(priority, skill));
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
