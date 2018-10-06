namespace Redninja.Events
{
	public class MovementEvent : IBattleEvent
	{
		public IBattleEntity Entity { get; }
		public EntityPosition NewPosition { get; }
		public EntityPosition OriginalPosition { get; }

		public MovementEvent(IBattleEntity entity, EntityPosition newPosition, EntityPosition originalPosition)
		{
			Entity = entity;
			NewPosition = newPosition;
			OriginalPosition = originalPosition;
		}
	}
}
