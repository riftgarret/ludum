using Redninja.Entities;

namespace Redninja.Targeting.Conditions
{
	/// <summary>
	/// Condition for a BattleEntity to be a target.
	/// </summary>
	public interface ITargetCondition
	{
		bool IsValidTarget(IBattleEntity entity);
	}
}
