namespace Redninja.Components.Combat.Events
{
	public interface ITargetedEvent : IBattleEvent
	{
		IUnitModel Target { get; }
	}
}
