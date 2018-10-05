using Redninja.BattleSystem.Entities;

namespace Redninja.BattleSystem.Targeting.Conditions
{
    /// <summary>
    /// Condition for a BattleEntity to be a target.
    /// </summary>
    public interface ITargetCondition
    {
        bool IsValidTarget(BattleEntity entity);
    }
}
