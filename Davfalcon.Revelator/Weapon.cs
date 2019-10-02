using System;
using System.Collections.Generic;
using Davfalcon.Builders;
using Davfalcon.Collections.Adapters;
using Davfalcon.Collections.Generic;
using Davfalcon.Serialization;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Weapon : Equipment, IWeapon
	{
		public Enum WeaponType { get; set; }
		public int BaseDamage { get; set; }
		public Enum BonusDamageStat { get; set; }
		public int CritMultiplier { get; set; } = 1;

		public ManagedEnumStringList DamageTypes { get; } = new ManagedEnumStringList();
		IEnumerable<Enum> IDamageSource.DamageTypes => DamageTypes.AsReadOnly();

		public ManagedList<IEffect> Effects { get; } = new ManagedList<IEffect>();
		IEnumerable<IEffect> IEffectSource.Effects => Effects.AsReadOnly();

		private string owner;
		public string Owner
		{
			get => owner ?? InterfaceUnit.Name;
			set => owner = value;
		}

		protected override IStatsPackage GetStatsResolver()
			=> GetStatsResolver<IWeapon>(this);

		protected Weapon(Enum equipmentSlot, Enum weaponType, IStatsOperations operations)
			: base(equipmentSlot, operations)
		{
			WeaponType = weaponType;
		}

		public static IWeapon Build(Enum equipmentSlot, Enum weaponType, Func<Builder, IBuilder<IWeapon>> builderFunc)
			=> Build(equipmentSlot, weaponType, StatsOperations.Default, builderFunc);

		public static IWeapon Build(Enum equipmentSlot, Enum weaponType, IStatsOperations operations, Func<Builder, IBuilder<IWeapon>> builderFunc)
			=> builderFunc(new Builder(equipmentSlot, weaponType, operations)).Build();

		new public class Builder : EquipmentBuilder<Weapon, IWeapon, Builder>
		{
			private readonly Enum type;

			internal Builder(Enum equipmentSlot, Enum weaponType, IStatsOperations operations)
				: base(equipmentSlot, operations)
			{
				type = weaponType;
				Reset();
			}

			public override Builder Reset() => Reset(new Weapon(slot, type, operations));

			public Builder SetDamage(int baseDamage) => Self(w => w.BaseDamage = baseDamage);
			public Builder SetBonusDamageStat(Enum stat) => Self(w => w.BonusDamageStat = stat);
			public Builder AddDamageType(Enum type) => Self(w => w.DamageTypes.Add(type));
			public Builder AddDamageTypes(params Enum[] types) => Self(w => w.DamageTypes.AddRange(EnumString.ConvertEnumArray(types)));
			public Builder SetCritMultiplier(int crit) => Self(w => w.CritMultiplier = crit);
			public Builder AddOnHitEffect(IEffect effect) => Self(w => w.Effects.Add(effect));
		}
	}
}
