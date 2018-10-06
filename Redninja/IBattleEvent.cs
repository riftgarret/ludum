using Redninja.Entities;
using System;
namespace Redninja
{
    /// <summary>
    /// Interface for Battle Events.
    /// </summary>
    public interface IBattleEvent
	{
		IBattleEntity Entity { get; }
    } 
}

