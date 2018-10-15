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
        public ActionPhase Phase { private set; get; }

        public ExecuteCombatOperation ExecuteCombatOperationDelegate { get; set; }

        public abstract float TimePrepare { get; }
        public abstract float TimeAction { get; }
        public abstract float TimeRecover { get; }

        protected abstract void ExecuteAction(float actionClock);

        /// <summary>
        /// Sets the phase. Reset all state variables
        /// </summary>
        /// <param name="phase">Phase.</param>
        protected void SetPhase(ActionPhase newPhase)
        {
            PhaseClock = 0;
            this.Phase = newPhase;
            switch (newPhase)
            {
                case ActionPhase.PREPARE:
                    PhaseComplete = TimePrepare;
                    break;
                case ActionPhase.EXECUTE:
                    PhaseComplete = TimeAction;
                    break;
                case ActionPhase.RECOVER:
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
                case ActionPhase.PREPARE:
                    SetPhase(ActionPhase.EXECUTE);
                    break;
                case ActionPhase.EXECUTE:
                    SetPhase(ActionPhase.RECOVER);
                    break;
                case ActionPhase.RECOVER:
                    // dont do anything, stay in this state
                    break;
            }

        }

        /// <summary>
        /// Increment Game states. This can update the current ActionPhase or even trigger
        /// the OnStartActionExecution delegate.
        /// </summary>
        /// <param name="gameClockDelta">Game clock delta.</param>
        public void IncrementGameClock(float gameClockDelta)
        {
            if (Phase == ActionPhase.RECOVER && PhasePercent >= 1f)
            {
                return;
            }


            PhaseClock += gameClockDelta;

            if (Phase == ActionPhase.EXECUTE)
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
