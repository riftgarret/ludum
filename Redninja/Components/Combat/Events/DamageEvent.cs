using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Combat.Events
{
	public enum DamageSourceType
	{
		Skill,
		Tick,
		Revenge
	}

	public class DamageEvent : ICombatEvent
	{		
		public IBattleEntity Source { get; }
		public IBattleEntity Target { get; }

		public DamageSourceType DamageSourceType { get; set; }

		public int TotalDamage { get => results.Sum(x => x.Value.Total);  }

		private IDictionary<DamageType, DamageOperationResult> results = new Dictionary<DamageType, DamageOperationResult>();

		internal DamageEvent(IBattleEntity source, IBattleEntity target)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Target = target ?? throw new ArgumentNullException(nameof(target));			
		}		

		public DamageOperationResult this[DamageType type]
		{
			get => results.ContainsKey(type)? results[type] : null;
		}

		public void PutResult(DamageType type, DamageOperationResult result) => results[type] = result;

		public int Total => results.Values.Sum(x => x.Total);

		private string DamageTypeDebug {
			get => results.Keys.Select(val => Enum.GetName(typeof(DamageType), val)).Aggregate((cur, next) => cur + ", " + next);
		}

		public override string ToString() => $"DamageEvent [{Source.Name} -> {Target.Name}] : {DamageTypeDebug} {Total} Damage from {Enum.GetName(typeof(DamageSourceType), DamageSourceType)}";
	}
}
