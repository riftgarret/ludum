using Redninja.BattleSystem.Entities;
using Redninja.Core;

namespace Redninja.BattleSystem.Targeting.Conditions
{
    /// <summary>
    /// Factory for creating common Conditions.
    /// </summary>
    public class TargetConditionFactory
    {
        public static ITargetCondition CreateMustBeAliveCondition()
            => new DelegateTargetCondition(e => e.Character.VolatileStats[CombatStats.HP] > 0);

        public static ITargetCondition CreateMustBeDeadCondition()
            => new DelegateTargetCondition(e => e.Character.VolatileStats[CombatStats.HP] <= 0);

        public static ITargetCondition CreateIsOnTeam(int teamId)
            => new DelegateTargetCondition(e => e.Team == teamId);

        /// <summary>
        /// Internal implementation to use delegates.
        /// </summary>
        public class DelegateTargetCondition : ITargetCondition
        {
            internal delegate bool Condition(IBattleEntity entity);
            private Condition conditionDelegate;
            internal DelegateTargetCondition(Condition condition) => this.conditionDelegate = condition;

            public bool IsValidTarget(BattleEntity entity) => conditionDelegate.Invoke(entity);            
        }
    }   
}
