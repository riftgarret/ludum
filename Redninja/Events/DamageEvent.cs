using System;
using System.Collections.Generic;
using Davfalcon;
using Davfalcon.Nodes;
using Davfalcon.Revelator.Combat;

namespace Redninja.Events
{
	public class DamageEvent : IBattleEvent
	{
		public IEntityModel Entity { get; }
		public INode Damage { get; }
		public IEnumerable<StatChange> StatChanges { get; }

		public DamageEvent(IEntityModel entity, INode damage, IEnumerable<StatChange> statChanges)
		{
			Entity = entity ?? throw new ArgumentNullException(nameof(entity));
			Damage = damage ?? throw new ArgumentNullException(nameof(damage));
			StatChanges = statChanges.ToNewReadOnlyCollectionSafe();
		}
	}
}
