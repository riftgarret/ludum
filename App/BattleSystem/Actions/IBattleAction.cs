
using App.BattleSystem.Combat.Operation;
using App.BattleSystem.Entity;

namespace App.BattleSystem.Actions
{
    public delegate void ExecuteCombatOperation(ICombatOperation operation);

    public interface IBattleAction
    {
        void IncrementGameClock(float actionClock);

        float TimePrepare { get; }
        float TimeAction { get; }
        float TimeRecover { get; }

        float PhaseClock { get; }
        float PhaseComplete { get; }
        float PhasePercent { get; }
        PhaseState Phase { get; }

        /// <summary>
        /// When this action has triggered. This can happen multiple times during
        /// the execute phase depending on skill meta.
        /// </summary>
        ExecuteCombatOperation ExecuteCombatOperationDelegate { get; set; }
    }
}


