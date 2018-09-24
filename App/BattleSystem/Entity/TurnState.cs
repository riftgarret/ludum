using UnityEngine;
using System.Collections;
using App.BattleSystem.Action;

namespace App.BattleSystem.Entity
{
    /// <summary>
    /// Turn state managed the time state when a entity should be in. There is 3 'charging' states and
    /// 1 immediate states:
    /// REQUIRES_INPUT (immediate), PREPARE (charged), EXECUTE (charged), RECOVER (charge).
    /// 
    /// At the end of RECOVER, an external component is required to reset the state machine
    /// to the beginning by setting a new Action.
    /// </summary>
    public class TurnState
    {
        // action phase
        public enum PhaseState
        {
            REQUIRES_INPUT,
            PREPARE,
            EXECUTE,
            RECOVER
        }

        // inner class to manage the turn state
        private IBattleAction action;       
        public IBattleAction Action { get => action; }

        public float turnClock;
        public float turnComplete;

        private PhaseState phase;
        public PhaseState Phase
        {
            get => phase;

            private set
            {
                turnClock = 0;
                this.phase = value;
                switch (value)
                {
                    case PhaseState.REQUIRES_INPUT:
                        action = null;
                        turnComplete = -1;
                        break;
                    case PhaseState.PREPARE:
                        turnComplete = action.TimePrepare;
                        break;
                    case PhaseState.EXECUTE:
                        turnComplete = action.TimeAction;
                        break;
                    case PhaseState.RECOVER:
                        turnComplete = action.TimeRecover;
                        break;
                }
            }
        }

        public delegate void OnStartActionExecution();

        public float TurnPercent
        {
            get
            {
                if (turnComplete == 0)
                {
                    return 0;
                }
                return Mathf.Min(turnClock / turnComplete, 1f);
            }
        }


        /// <summary>
        /// Sets the action. This will reset all variables to Prepare
        /// </summary>
        /// <param name="action">Action.</param>
        public void SetAction(IBattleAction action)
        {
            this.action = action;
            SetPhase(PhaseState.PREPARE);
        }

        private void SanityCheck(IBattleAction action)
        {
            if(action.TimeAction <= 0)
            {
                Debug.LogWarning("Invalid Time: Action");
            }

            if (action.TimeRecover <= 0)
            {
                Debug.LogWarning("Invalid Time: Recover");
            }

            if (action.TimePrepare <= 0)
            {
                Debug.LogWarning("Invalid Time: Prepare");
            }
        }

        /// <summary>
        /// Sets the phase. Reset all state variables
        /// </summary>
        /// <param name="phase">Phase.</param>
        private void SetPhase(PhaseState newPhase)
        {
                      
        }

        /// <summary>
        /// Checks the next phase. Sometimes we can start with 0 turn, lets skip that
        /// </summary>
        private void CheckNextPhase(bool force)
        {
            if (force || (Phase != PhaseState.REQUIRES_INPUT && turnComplete == 0))
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
                        SetPhase(PhaseState.REQUIRES_INPUT);
                        break;
                }
            }
        }

        /// <summary>
        /// Increment Game states. This can update the current PhaseState or even trigger
        /// the OnStartActionExecution delegate.
        /// </summary>
        /// <param name="gameClockDelta">Game clock delta.</param>
        public void IncrementGameClock(float gameClockDelta)
        {
            if (Phase != PhaseState.REQUIRES_INPUT)
            {
                turnClock += gameClockDelta;

                if (turnClock > turnComplete)
                {
                    // increment turn
                    CheckNextPhase(true);                                   
                }
            }
        }
    } 
}
