using System;
using System.Collections.Generic;
using Davfalcon;
using Davfalcon.Nodes;
using Davfalcon.Revelator.Combat;

namespace Redninja.Components.Combat.Events
{
	public class DamageEvent : ICombatEvent
	{
		public IBattleEntity Source { get; }
		public IBattleEntity Target { get; }
		public INode Damage { get; }
		public IEnumerable<StatChange> StatChanges { get; }

		internal DamageEvent(IBattleEntity source, IBattleEntity target, INode damage, IEnumerable<StatChange> statChanges)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Target = target ?? throw new ArgumentNullException(nameof(target));
			Damage = damage ?? throw new ArgumentNullException(nameof(damage));
			StatChanges = statChanges.ToNewReadOnlyCollectionSafe();
		}
	}
}
