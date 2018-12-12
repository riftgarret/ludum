using System;

namespace Redninja.Components.Combat.Events
{
	public class MovementEvent : ICombatEvent
	{
		public IUnitModel Source { get; }
		public IUnitModel Target => null;
		public UnitPosition NewPosition { get; }
		public UnitPosition OriginalPosition { get; }

		internal MovementEvent(IUnitModel entity, UnitPosition newPosition, UnitPosition originalPosition)
		{
			Source = entity ?? throw new ArgumentNullException(nameof(entity));
			NewPosition = newPosition;
			OriginalPosition = originalPosition;
		}
	}
}
