using System;
using System.Collections.Generic;
using Davfalcon;
using Davfalcon.Nodes;

namespace Redninja.Components.Combat.Events
{
	public class DamageEvent : ICombatEvent
	{
		public IBattleEntity Source { get; }
		public IBattleEntity Target { get; }		

		internal DamageEvent(IBattleEntity source, IBattleEntity target)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Target = target ?? throw new ArgumentNullException(nameof(target));			
		}
	}
}
