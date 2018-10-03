using System;
using Redninja.BattleSystem.Combat.Operation;
using Redninja.BattleSystem.Entities;

namespace Redninja.BattleSystem.Actions
{
	/// <summary>
	/// Base implementation that handles incrementing time. It is required by the 
	/// subclass to call SetPhase() as part of its initialization.
	/// </summary>
	public abstract class BaseBattleAction : IBattleAction
    {
		private float clock;

        public float PhaseComplete { get; private set; }
        public float PhasePercent => PhaseComplete == 0 ? 1f : Math.Min(clock / PhaseComplete, 1f);
        public PhaseState Phase { private set; get; }

		// This function signature needs to be changed completely
		public event Action<BattleEntity, ICombatOperation> ActionExecuted;

		// Make these regular properties
        public abstract float TimePrepare { get; }
        public abstract float TimeAction { get; }
        public abstract float TimeRecover { get; }

        protected abstract void ExecuteAction(float timeDelta, float time);

        /// <summary>
        /// Sets the phase. Reset all state variables
        /// </summary>
        /// <param name="phase">Phase.</param>
        protected void SetPhase(PhaseState newPhase)
        {
			clock = 0;
            Phase = newPhase;
            switch (newPhase)
            {
                case PhaseState.Preparing:
                    PhaseComplete = TimePrepare;
                    break;
                case PhaseState.Executing:
                    PhaseComplete = TimeAction;
                    break;
                case PhaseState.Recovering:
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
                case PhaseState.Preparing:
                    SetPhase(PhaseState.Executing);
                    break;
                case PhaseState.Executing:
                    SetPhase(PhaseState.Recovering);
                    break;
                case PhaseState.Recovering:
                    // dont do anything, stay in this state
                    break;
            }
        }

        /// <summary>
        /// Increment Game states. This can update the current PhaseState or even trigger
        /// the OnStartActionExecution delegate.
        /// </summary>
        /// <param name="gameClockDelta">Game clock delta.</param>
        public void Tick(float timeDelta, float time)
        {
            if (Phase == PhaseState.Recovering && PhasePercent >= 1f)
            {
                return;
            }


			clock += timeDelta;

            if (Phase == PhaseState.Executing)
            {
				// This should probably return the result of the action
                ExecuteAction(PhasePercent);

				// Raise event here (placeholder)
				ActionExecuted?.Invoke(null, null);
            }

            if (clock >= PhaseComplete)
            {
                // increment turn
                IncrementPhase();
            }

        }
    }
}
