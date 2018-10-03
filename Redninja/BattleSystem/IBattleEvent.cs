using Redninja.BattleSystem.Entities;
using System;
namespace Redninja.BattleSystem
{
    /// <summary>
    /// Interface for Battle Events.
    /// </summary>
    public interface IBattleEvent
	{
		IBattleEntity Entity { get; }
    } 
}

