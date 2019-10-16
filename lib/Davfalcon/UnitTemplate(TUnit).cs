using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Davfalcon.Stats;

namespace Davfalcon
{
	/// <summary>
	/// Implements basic unit functionality.
	/// </summary>
	/// <typeparam name="TUnit">The interface used by the unit's implementation.</typeparam>
	[Serializable]
	public abstract class UnitTemplate<TUnit> : IUnitTemplate<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		protected interface IUnitStats : IStatsEditable, IStatsProperties { }

		[Serializable]
		protected class UnitStats : StatsMap, IUnitStats
		{
			private readonly UnitTemplate<TUnit> unit;

			public IStats Base => this;

			public IStatNode GetStatNode(Enum stat) => new StatNode(stat.ToString(), Base[stat]);

			public override int Get(Enum stat)
				=> unit.StatDerivations.ContainsKey(stat) ? unit.StatDerivations[stat](this) : base.Get(stat);

			public int GetModificationBase(Enum stat)
				=> unit.StatDerivations.ContainsKey(stat) ? unit.StatDerivations[stat](unit.Modifiers.AsModified().Stats) : base.Get(stat);

			public UnitStats(UnitTemplate<TUnit> unit)
			{
				this.unit = unit;
			}
		}

		private IUnitStats stats;
		private readonly Dictionary<Enum, IUnitComponent<TUnit>> components = new Dictionary<Enum, IUnitComponent<TUnit>>();

		public string Name { get; set; }

		public IStatsEditable BaseStats => stats;

		public IStatsProperties Stats => stats;

		public IDictionary<Enum, Func<IStatsProperties, int>> StatDerivations { get; } = new Dictionary<Enum, Func<IStatsProperties, int>>();

		public IModifierStack<TUnit> Modifiers { get; private set; }

		protected abstract TUnit Self { get; }

		protected virtual IUnitStats InitializeUnitStats() => new UnitStats(this);

		protected virtual IModifierStack<TUnit> InitializeModifierStack() => new ModifierStack<TUnit>();

		public void AddComponent(Enum id, IUnitComponent<TUnit> component)
		{
			components.Add(id, component);
			component.Initialize(Self);
		}

		public TComponent GetComponent<TComponent>(Enum id) where TComponent : class, IUnitComponent<TUnit>
			=> components[id] as TComponent;

		protected virtual void Setup()
		{
			stats = InitializeUnitStats();
			Modifiers = InitializeModifierStack();
		}

		protected virtual void Link()
		{
			// This will initiate the modifier rebinding calls
			Modifiers.Bind(Self);
		}

		[OnDeserialized]
		private void Rebind(StreamingContext context)
		{
			// Reset object references after deserialization
			Link();
		}

		public UnitTemplate()
		{
			Setup();
			Link();
		}
	}
}
