﻿using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Conditions;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions.AI
{
	/// <summary>
	/// Base class for AI rule that contains all the similar parts.
	/// </summary>
	public abstract class AIRuleBase : IAIRule
	{
		// trigger conditions can rely on different targets
		private List<ICondition> triggerConditions = new List<ICondition>();
		public IEnumerable<ICondition> TriggerConditions => triggerConditions;

		public string RuleName { get; private set; } = "Unnamed Rule";

		public int Weight { get; private set; }

		public int RefreshTime { get; private set; }
		
		/// <summary>
		/// Builder class for a rule.
		/// </summary>
		public abstract class BuilderBase<ParentBuilder, T> : IBuilder<T>
			where ParentBuilder:BuilderBase<ParentBuilder, T>
			where T:AIRuleBase
		{
			private AIRuleBase rule;
			
			protected ParentBuilder ResetBase(AIRuleBase rule)
			{
				this.rule = rule;
				return this as ParentBuilder;
			}
			
			/// <summary>
			/// Trigger conditions pairs can be any target meating a condition.
			/// </summary>
			/// <param name="type"></param>
			/// <param name="condition"></param>
			/// <returns></returns>
			public ParentBuilder AddTriggerCondition(ICondition condition)
			{
				rule.triggerConditions.Add(condition);
				return this as ParentBuilder;
			}

			/// <summary>
			/// Sets the name, mostly useful for debugging purposes.
			/// </summary>
			/// <param name="name"></param>
			/// <returns></returns>
			public ParentBuilder SetName(string name)
			{
				rule.RuleName = name;
				return this as ParentBuilder;
			}

			/// <summary>
			/// Weight that represents the chance in a sum of weights to be used
			/// </summary>
			/// <param name="weight"></param>
			/// <returns></returns>
			public ParentBuilder SetWeight(int weight)
			{
				rule.Weight = weight;
				return this as ParentBuilder;
			}

			/// <summary>
			/// Refresh time is how frequent this rule can trigger successfully.
			/// </summary>
			/// <param name="time"></param>
			/// <returns></returns>
			public ParentBuilder SetRefreshTime(int time)
			{
				rule.RefreshTime = time;
				return this as ParentBuilder;
			}

			protected void BuildBase()
			{
				// validation check				
				if (rule.Weight <= 0) throw new InvalidOperationException($"Invalid weight, must be > 0 for Rule: {rule.RuleName}");				
			}

			public abstract T Build();
		}
	}
}
