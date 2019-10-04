using System;
using System.Collections.Generic;
using Davfalcon.Builders;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Equipment : UnitStatsModifier, IEquipment
	{
		public Enum SlotType { get; set; }

		public ManagedList<IBuff> GrantedBuffs { get; } = new ManagedList<IBuff>();
		IEnumerable<IBuff> IEquipment.GrantedBuffs => GrantedBuffs.AsReadOnly();

		protected override IStatsPackage GetStatsResolver()
			=> GetStatsResolver<IEquipment>(this);

		protected Equipment(Enum slot, IStatsOperations operations)
			: base(operations)
		{
			SlotType = slot;
		}

		public static IEquipment Build(Enum slot, Func<Builder, IBuilder<IEquipment>> builderFunc)
			=> Build(slot, StatsOperations.Default, builderFunc);

		public static IEquipment Build(Enum slot, IStatsOperations operations, Func<Builder, IBuilder<IEquipment>> builderFunc)
			=> builderFunc(new Builder(slot, operations)).Build();

		public abstract class EquipmentBuilder<TEquipment, TInterface, TBuilder> : BuilderBase<TEquipment, TInterface, TBuilder>
			where TEquipment : Equipment, TInterface
			where TInterface : IEquipment
			where TBuilder : EquipmentBuilder<TEquipment, TInterface, TBuilder>
		{
			protected readonly Enum slot;
			protected readonly IStatsOperations operations;
			
			protected EquipmentBuilder(Enum slot, IStatsOperations operations)
			{
				this.slot = slot;
				this.operations = operations;
			}

			public TBuilder SetName(string name) => Self(e => e.Name = name);
			public TBuilder SetStatAddition(Enum stat, int value) => Self(e => e.Additions[stat] = value);
			public TBuilder SetStatMultiplier(Enum stat, int value) => Self(e => e.Multipliers[stat] = value);
			public TBuilder AddBuff(IBuff buff) => Self(e => e.GrantedBuffs.Add(buff));
		}

		public class Builder : EquipmentBuilder<Equipment, IEquipment, Builder>
		{
			internal Builder(Enum slot, IStatsOperations operations)
				: base(slot, operations) => Reset();

			public override Builder Reset() => Reset(new Equipment(slot, operations));
		}
	}
}
