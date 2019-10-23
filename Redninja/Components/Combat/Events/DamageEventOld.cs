using System;
using System.Collections.Generic;
using Davfalcon;
using Davfalcon.Nodes;

namespace Redninja.Components.Combat.Events
{
	public class DamageEventOld : ICombatEvent
	{
		public IBattleEntity Source { get; }
		public IBattleEntity Target { get; }		

		internal DamageEventOld(IBattleEntity source, IBattleEntity target)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Target = target ?? throw new ArgumentNullException(nameof(target));			
		}
	}
}
