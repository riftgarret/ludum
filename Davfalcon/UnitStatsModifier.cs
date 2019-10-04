using System;
using Davfalcon.Nodes;
using Davfalcon.Stats;

namespace Davfalcon
{
	/// <summary>
	/// Modify a unit's stats.
	/// </summary>
	[Serializable]
	public class UnitStatsModifier : UnitModifier, IEditableStatsModifier, IStatsModifier, IUnit
	{
		private class StatsResolver<T> : IStatsPackage where T : IStatsModifier
		{
			private readonly T modifier;
			private readonly IStatsOperations operations;

			private IStatsPackage TargetDetails => modifier.Target.StatsDetails;

			public IStats Base => TargetDetails.Base;
			public IStats Additions { get; }
			public IStats Multipliers { get; }
			public IStats Final { get; }

			public INode GetBaseStatNode(Enum stat)
				=> TargetDetails.GetBaseStatNode(stat);

			public IAggregatorNode GetAdditionsNode(Enum stat)
				=> modifier.Additions[stat] != 0
					? TargetDetails.GetAdditionsNode(stat).Merge(StatNode<T>.From(modifier, modifier.Additions, stat))
					: TargetDetails.GetAdditionsNode(stat);

			public IAggregatorNode GetMultipliersNode(Enum stat)
				=> modifier.Multipliers[stat] != operations.AggregateSeed
					? TargetDetails.GetMultipliersNode(stat).Merge(StatNode<T>.From(modifier, modifier.Multipliers, stat))
					: TargetDetails.GetMultipliersNode(stat);

			public INode GetStatNode(Enum stat)
				=> new ResolverNode($"{stat} ({modifier.Target.Name})",
					GetBaseStatNode(stat),
					GetAdditionsNode(stat),
					GetMultipliersNode(stat),
					operations);

			public StatsResolver(T modifier, IStatsOperations operations)
			{
				this.modifier = modifier;
				this.operations = operations;

				// Always use default aggregator for addition (sum)
				Additions = new StatsAggregator(TargetDetails.Additions, modifier.Additions);
				Multipliers = new StatsAggregator(TargetDetails.Multipliers, modifier.Multipliers, operations);

				// The final calculation will use the defined formula (or default if not specified)
				Final = new StatsCalculator(Base, Additions, Multipliers, operations);
			}
		}

		[NonSerialized]
		private IStatsPackage statsResolver;

		private readonly IStatsOperations operations;

		protected virtual IStatsPackage GetStatsResolver()
			=> GetStatsResolver<IStatsModifier>(this);

		protected IStatsPackage GetStatsResolver<T>(T modifier) where T : IStatsModifier
			=> new StatsResolver<T>(modifier, operations);

		/// <summary>
		/// Gets or sets the values to be added to each stat.
		/// </summary>
		public IEditableStats Additions { get; set; }

		/// <summary>
		/// Gets or sets the values to be multiplied with each stat.
		/// </summary>
		public IEditableStats Multipliers { get; set; }

		/// <summary>
		/// Binds the modifier to an <see cref="IUnit"/>.
		/// </summary>
		/// <param name="target">The <see cref="IUnit"/> to bind the modifier to.</param>
		public override void Bind(IUnit target)
		{
			base.Bind(target);
			statsResolver = GetStatsResolver();
		}

		IStats IStatsHolder.Stats => statsResolver.Final;
		IStatsPackage IStatsHolder.StatsDetails => statsResolver;

		IStats IStatsModifier.Additions => Additions;
		IStats IStatsModifier.Multipliers => Multipliers;

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitStatsModifier"/> class.
		/// </summary>
		public UnitStatsModifier()
			: this(StatsOperations.Default)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitStatsModifier"/> class with the specified <see cref="IStatsOperations"/>.
		/// </summary>
		/// <param name="operations">The stat operations definition.</param>
		public UnitStatsModifier(IStatsOperations operations)
		{
			Additions = new StatsMap();
			Multipliers = new StatsMap();
			this.operations = operations;
		}
	}
}
