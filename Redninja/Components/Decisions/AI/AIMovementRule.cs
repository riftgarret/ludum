using Davfalcon.Builders;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions.AI
{
	/// <summary>
	/// This represents a set of conditions that may be attached to a list of
	/// actionable items. All skills in this rule should be unique.
	/// </summary>
	public class AIMovementRule : AIRuleBase
	{
					
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
