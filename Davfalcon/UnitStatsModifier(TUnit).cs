using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Nodes;
using Davfalcon.Stats;

namespace Davfalcon
{
	/// <summary>
	/// Abstract base stats for modifiers affecting unit stats.
	/// </summary>
	/// <typeparam name="TUnit">The interface used by the unit's implementation.</typeparam>
	[Serializable]
	public abstract class UnitStatsModifier<TUnit> : UnitModifier<TUnit>, IStatsModifier<TUnit>, IUnitTemplate<TUnit>
		where TUnit : class, IUnitTemplate<TUnit>
	{
		[Serializable]
		private class UnitStatsProxy : StatsPrototype, IStatsProperties
		{
			private readonly UnitStatsModifier<TUnit> modifier;

			public IStats Base => modifier.GetBaseStats();

			public int GetModificationBase(Enum stat, IStatsProperties modified) => modifier.GetModificationBaseStat(stat, modified);

			public IStatNode GetStatNode(Enum stat) => modifier.GetStatNode(stat);

			public override int Get(Enum stat) => GetStatNode(stat).Value;

			public UnitStatsProxy(UnitStatsModifier<TUnit> modifier)
			{
				this.modifier = modifier;
			}
		}

		private readonly UnitStatsProxy statsProxy;

		public IDictionary<Enum, IStatsEditable> StatModifications { get; } = new Dictionary<Enum, IStatsEditable>();

		IStatsProperties IUnitTemplate<TUnit>.Stats => statsProxy;

		public void AddStatModificationType(Enum type) => StatModifications.Add(type, new StatsMap());

		public IStats GetStatModifications(Enum type) => StatModifications[type];

		protected virtual IStatNode GetStatNode(Enum stat)
		{
			IStatNode targetStatNode = Target.Stats.GetStatNode(stat);
			return new StatNode(stat.ToString(), GetModificationBaseStat(stat, AsModified().Stats), Resolve,
				StatModifications.ToDictionary(kvp => kvp.Key, kvp =>
				{
					INode<int> prev = targetStatNode.GetModification(kvp.Key);

					List<INode<int>> mods = new List<INode<int>>();
					if (prev != null)
					{
						mods.AddRange(prev);
					}

					mods.Add(new ValueNode<int>(kvp.Value[stat]));

					return new AggregatorNode<int>(mods, GetAggregator(kvp.Key), GetAggregatorSeed(kvp.Key)) as INode<int>;
				})
			);
		}

		protected IStats GetBaseStats() => Target.Stats.Base;

		protected int GetModificationBaseStat(Enum stat, IStatsProperties modified) => Target.Stats.GetModificationBase(stat, modified);

		protected abstract int Resolve(int baseValue, IReadOnlyDictionary<Enum, int> modifications);

		protected abstract Func<int, int, int> GetAggregator(Enum modificationType);

		protected abstract int GetAggregatorSeed(Enum modificationType);

		public override void Bind(TUnit target)
		{
			base.Bind(target);
		}

		public UnitStatsModifier()
		{
			statsProxy = new UnitStatsProxy(this);
		}
	}
}
