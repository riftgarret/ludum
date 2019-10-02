using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Spell : ISpell, IEditableDescription
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Owner { get; set; }

		public Enum TargetType { get; set; }
		public int Cost { get; set; }
		public int BaseDamage { get; set; }
		public Enum BonusDamageStat { get; set; }
		public int CritMultiplier { get; set; }
		public IEnumerable<Enum> DamageTypes { get; set; }
		public int BaseHeal { get; set; }
		public int Range { get; set; }
		public int Size { get; set; }
		public int MaxTargets { get; set; }

		public ManagedList<IBuff> GrantedBuffs { get; } = new ManagedList<IBuff>();
		IEnumerable<IBuff> ISpell.GrantedBuffs => GrantedBuffs.AsReadOnly();

		public ManagedList<IEffect> Effects { get; } = new ManagedList<IEffect>();
		IEnumerable<IEffect> IEffectSource.Effects => Effects.AsReadOnly();
	}
}
