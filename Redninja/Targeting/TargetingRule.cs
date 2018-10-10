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
		public TargetTeam Team { get; }

		public ITargetPattern Pattern { get; }

		private TargetingRule(TargetType targetType, TargetTeam team, ITargetPattern targetPattern, TargetCondition condition)
		{
			this.condition = condition ?? throw new ArgumentNullException(nameof(condition));
			Type = targetType;
			Pattern = targetPattern;
		}

		public TargetingRule(ITargetPattern targetPattern, TargetTeam team, TargetCondition condition)
			: this(TargetType.Pattern, team, targetPattern, condition)
		{ }

		public TargetingRule(TargetTeam team, TargetCondition condition)
			: this(TargetType.Entity, team, null, condition)
		{ }

		/// <summary>
		/// Determines whether this instance is valid target the specified entity.
		/// </summary>
		/// <returns><c>true</c> if this instance is valid target the specified entity; otherwise, <c>false</c>.</returns>
		/// <param name="entity">Entity.</param>
		public bool IsValidTarget(IBattleEntity target, IBattleEntity user)
			=> condition(target, user);
	}
}
