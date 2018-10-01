using System;
using Redninja.BattleSystem.Combat.Operation;
using Redninja.BattleSystem.Entity;

namespace Redninja.BattleSystem.Actions
{
	/// <summary>
	/// Base implementation that handles incrementing time. It is required by the 
	/// subclass to call SetPhase() as part of its initialization.
	/// </summary>
	public abstract class BaseBattleAction : IBattleAction
    {
        public float PhaseClock { get; private set; }	// This property probably doesn't need to be public
        public float PhaseComplete { get; private set; }
        public float PhasePercent => PhaseComplete == 0 ? 1f : Math.Min(PhaseClock / PhaseComplete, 1f);
        public PhaseState Phase { private set; get; }

		// This function signature needs to be changed completely
		public event Action<BattleEntity, ICombatOperation> ActionExecuted;

		// Make these regular properties
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
			// I think you can just use Phase++ here plus a check for RECOVER
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
				// This should probably return the result of the action
                ExecuteAction(PhasePercent);

				// Raise event here (placeholder)
				ActionExecuted?.Invoke(null, null);
            }

            if (PhaseClock >= PhaseComplete)
            {
                // increment turn
                IncrementPhase();
            }

        }
    }
}
