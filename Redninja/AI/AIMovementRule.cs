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
	public class AIMovementRule : AIRuleBase
	{
		
		public override IBattleAction GenerateAction(IBattleEntity source, IBattleEntityManager bem)
		{
			// psuedo code for now
			// 1. Find available tiles you can move to (cannot be occupied or have units currently moving there)
			// 2. Select position based on priority rule
			return null;
		}
		

		/// <summary>
		/// Builder class for a rule.
		/// </summary>
		public class Builder : AIRuleBase.BuilderBase<Builder>, IBuilder<AIMovementRule>
		{
			private AIMovementRule rule;

			public Builder() => Reset();

			public Builder Reset()
			{
				rule = new AIMovementRule();
				ResetBase(rule);
				return this;
			}
					

			public AIMovementRule Build()
			{
				BuildBase();
				AIMovementRule builtRule = this.rule;
				Reset();
				return builtRule;
			}
		}
	}
}