using System;

namespace Redninja.Events
{
	public class MovementEvent : IBattleEvent
	{
		public IUnitModel Entity { get; }
		public UnitPosition NewPosition { get; }
		public UnitPosition OriginalPosition { get; }

		public MovementEvent(IUnitModel entity, UnitPosition newPosition, UnitPosition originalPosition)
		{
			Entity = entity ?? throw new ArgumentNullException(nameof(entity));
			NewPosition = newPosition;
			OriginalPosition = originalPosition;
		}
	}
}
