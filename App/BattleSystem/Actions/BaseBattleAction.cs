using App.BattleSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace App.BattleSystem.Actions
{
    /// <summary>
    /// Base implementation that handles incrementing time. It is required by the 
    /// subclass to call SetPhase() as part of its initialization.
    /// </summary>
    public abstract class BaseBattleAction : IBattleAction
    {
        // State for this action
        public float PhaseClock { get; private set; }
        public float PhaseComplete { get; private set; }
        public float PhasePercent => PhaseComplete == 0 ? 1f : Mathf.Min(PhaseClock / PhaseComplete, 1f);
        public PhaseState Phase { private set; get; }

        public ExecuteCombatOperation ExecuteCombatOperationDelegate { get; set; }

        public abstract float TimePrepare { get; }
        public abstract float TimeAction { get; }
        public abstract float TimeRecover { get; }

        protected abstract void ExecuteAction(float actionClock);

        /// <summary>
        /// Sets the phase. Reset all state variables
        /// </summary>
        /// <param name="phase">Phase.</param>
        protected void SetPhase(PhaseState newPhase)
        {
            PhaseClock = 0;
            this.Phase = newPhase;
            switch (newPhase)
            {
                case PhaseState.PREPARE:
                    PhaseComplete = TimePrepare;
                    break;
                case PhaseState.EXECUTE:
                    PhaseComplete = TimeAction;
                    break;
                case PhaseState.RECOVER:
                    PhaseComplete = TimeRecover;
                    break;
            }
        }

        /// <summary>
        /// Checks the next phase. Sometimes we can start with 0 turn, lets skip that
        /// </summary>
        private void IncrementPhase()
        {
            switch (Phase)
            {
                case PhaseState.PREPARE:
                    SetPhase(PhaseState.EXECUTE);
                    break;
                case PhaseState.EXECUTE:
                    SetPhase(PhaseState.RECOVER);
                    break;
                case PhaseState.RECOVER:
                    // dont do anything, stay in this state
                    break;
            }

        }

        /// <summary>
        /// Increment Game states. This can update the current PhaseState or even trigger
        /// the OnStartActionExecution delegate.
        /// </summary>
        /// <param name="gameClockDelta">Game clock delta.</param>
        public void IncrementGameClock(float gameClockDelta)
        {
            if (Phase == PhaseState.RECOVER && PhasePercent >= 1f)
            {
                return;
            }


            PhaseClock += gameClockDelta;

            if (Phase == PhaseState.EXECUTE)
            {
                ExecuteAction(PhasePercent);
            }

            if (PhaseClock >= PhaseComplete)
            {
                // increment turn
                IncrementPhase();
            }

        }
    }
}
