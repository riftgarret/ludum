using System;

namespace Redninja.Targeting
{
	/// <summary>
	/// Targeting Rule represents a collection of conditions and target type.
	/// </summary>
	public class TargetingRule : ITargetingRule
	{
		private readonly TargetCondition condition;

		public TargetType Type { get; }

		public int MaxTargets { get; }

		public ITargetPattern Pattern { get; }

		private TargetingRule(TargetType targetType, int maxTargets, ITargetPattern targetPattern, TargetCondition condition)
		{
			this.condition = condition ?? throw new ArgumentNullException();
			Type = TargetType.Pattern;
			Pattern = targetPattern;
		}

		public TargetingRule(ITargetPattern targetPattern, TargetCondition condition)
			: this(TargetType.Pattern, 1, targetPattern, condition)
		{ }

		public TargetingRule(int maxTargets, TargetCondition condition)
			: this(TargetType.Entity, maxTargets, null, condition)
		{ }

		public TargetingRule(TargetCondition condition)
			: this(1, condition)
		{ }

		/// <summary>
		/// Determines whether this instance is valid target the specified entity.
		/// </summary>
		/// <returns><c>true</c> if this instance is valid target the specified entity; otherwise, <c>false</c>.</returns>
		/// <param name="entity">Entity.</param>
		public bool IsValidTarget(IBattleEntity entity)
			=> condition(entity);
	}
}
