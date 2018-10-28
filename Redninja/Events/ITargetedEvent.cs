using System;
namespace Redninja.Events
{
	public interface ITargetedEvent : IBattleEvent
	{
		IUnitModel Source { get; }
		IUnitModel Target { get; }
	}
}
