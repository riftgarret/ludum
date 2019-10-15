using System;

namespace Redninja.Components.Combat.Events
{
	public class MovementEvent : ICombatEvent
	{
		public IBattleEntity Source { get; }
		public IBattleEntity Target => null;
		public UnitPosition NewPosition { get; }
		public UnitPosition OriginalPosition { get; }

		internal MovementEvent(IBattleEntity entity, UnitPosition newPosition, UnitPosition originalPosition)
		{
			Source = entity ?? throw new ArgumentNullException(nameof(entity));
			NewPosition = newPosition;
			OriginalPosition = originalPosition;
		}
	}
}
