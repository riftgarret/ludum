using System;
using System.Collections;
using UnityEngine;

namespace App.BattleSystem.Effects
{
    public interface IStatusEffectRunner : IStatusEffect
    {
        BattleEntity SourceEntity
        {
            get;
        }

        /// <summary>
        /// Gets the total length of the duration. How long should it exist before it removes naturally
        /// </summary>
        /// <value>The total length of the duration.</value>
        float TotalDurationLength
        {
            get;
        }

        /// <summary>
        /// Gets the length of the current duration.
        /// </summary>
        /// <value>The length of the current duration.</value>
        float CurrentDurationLength
        {
            get;
        }

        bool IsExpired
        {
            get;
        }

        /// <summary>
        /// Increments the duration time.
        /// </summary>
        /// <param name="timeDelta">Time delta.</param>
        void IncrementDurationTime(float timeDelta);
    } 
}


