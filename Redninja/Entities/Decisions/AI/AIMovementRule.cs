using Davfalcon.Builders;
using Redninja.Components.Actions;

namespace Redninja.Entities.Decisions.AI
{
	/// <summary>
	/// This represents a set of conditions that may be attached to a list of
	/// actionable items. All skills in this rule should be unique.
	/// </summary>
	public class AIMovementRule : AIRuleBase
	{
		
		public override IBattleAction GenerateAction(IEntityModel source, IDecisionHelper decisionHelper)
		{
			// psuedo code for now
			// 1. Find available tiles you can move to (cannot be occupied or have units currently moving there)
			// 2. Select position based on priority rule
			return null;
		}


		/// <summary>
		/// Builder class for a rule.
		/// </summary>
		public class Builder : BuilderBase<Builder, AIMovementRule>, IBuilder<AIMovementRule>
		{
			private AIMovementRule rule;

			public Builder() => Reset();

			public Builder Reset()
			{
				rule = new AIMovementRule();
				ResetBase(rule);
				return this;
			}
					
			public override AIMovementRule Build()
			{
				BuildBase();
				AIMovementRule builtRule = this.rule;
				Reset();
				return builtRule;
			}
		}
	}
}
