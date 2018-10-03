using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Redninja.BattleSystem.Entities;

namespace Redninja.BattleSystem.Targeting
{
    public interface IBattleTargetProvider
    {
        /// <summary>
        /// Gets the targets of specified type.
        /// </summary>
        /// <returns>The targets.</returns>
        /// <param name="targetType">Target type.</param>
        BattleEntity[] GetTargets(bool pcEntities);

        BattleEntity[] GetAllTargets();
    } 
}