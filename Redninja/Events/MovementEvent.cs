using System;

namespace Redninja.Events
{
	public class MovementEvent : IBattleEvent
	{
		public IEntityModel Entity { get; }
		public EntityPosition NewPosition { get; }
		public EntityPosition OriginalPosition { get; }

		public MovementEvent(IEntityModel entity, EntityPosition newPosition, EntityPosition originalPosition)
		{
			Entity = entity ?? throw new ArgumentNullException(nameof(entity));
			NewPosition = newPosition;
			OriginalPosition = originalPosition;
		}
	}
}
