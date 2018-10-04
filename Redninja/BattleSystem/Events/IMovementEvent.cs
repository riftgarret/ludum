namespace Redninja.BattleSystem.Events
{
	public interface IMovementEvent : IBattleEvent
	{
		EntityPosition NewPosition { get; }
		EntityPosition OriginalPosition { get; }
	}
}
