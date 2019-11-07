using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Combat.Events
{
	public class DamageEvent : ICombatEvent
	{
		public IBattleEntity Source { get; }
		public IBattleEntity Target { get; }

		public int TotalDamage { get => results.Sum(x => x.Value.Total);  }

		private IDictionary<DamageType, DamageResult> results = new Dictionary<DamageType, DamageResult>();

		internal DamageEvent(IBattleEntity source, IBattleEntity target)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Target = target ?? throw new ArgumentNullException(nameof(target));			
		}		

		public DamageResult this[DamageType type]
		{
			get => results.ContainsKey(type)? results[type] : null;
		}

		public void PutResult(DamageType type, DamageResult result) => results[type] = result;

		public int Total => results.Values.Sum(x => x.Total);
	}
}
