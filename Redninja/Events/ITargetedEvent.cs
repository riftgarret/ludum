using System;
namespace Redninja.Events
{
	public interface ITargetedEvent : IBattleEvent
	{
		IUnitModel Target { get; }
	}
}
