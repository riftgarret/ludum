using System;
using System.Collections.Generic;
using Davfalcon;
using Davfalcon.Nodes;
using Davfalcon.Revelator.Combat;

namespace Redninja.Components.Combat.Events
{
	public class DamageEvent : ICombatEvent
	{
		public IUnitModel Source { get; }
		public IUnitModel Target { get; }
		public INode Damage { get; }
		public IEnumerable<StatChange> StatChanges { get; }

		internal DamageEvent(IUnitModel source, IUnitModel target, INode damage, IEnumerable<StatChange> statChanges)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Target = target ?? throw new ArgumentNullException(nameof(target));
			Damage = damage ?? throw new ArgumentNullException(nameof(damage));
			StatChanges = statChanges.ToNewReadOnlyCollectionSafe();
		}
	}
}
