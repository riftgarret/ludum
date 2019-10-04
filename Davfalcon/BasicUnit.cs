using System;
using System.Runtime.Serialization;
using Davfalcon.Nodes;
using Davfalcon.Stats;

namespace Davfalcon
{
	/// <summary>
	/// Implements basic unit functionality.
	/// </summary>
	[Serializable]
	public class BasicUnit : IUnit, IEditableName
	{
		private class BaseStatsRouter : IStatsPackage
		{
			private BasicUnit unit;

			public IStats Base => unit.BaseStats;
			public IStats Additions => StatsConstant.Zero;
			public IStats Multipliers => new StatsConstant(unit.aggregator.AggregateSeed);
			public IStats Final => unit.Modifiers.Stats;

			public INode GetBaseStatNode(Enum stat) => StatNode<IUnit>.From(unit, unit.BaseStats, stat);
			public IAggregatorNode GetAdditionsNode(Enum stat) => new AggregatorNode($"{stat} additions ({unit.Name})", null);
			public IAggregatorNode GetMultipliersNode(Enum stat) => new AggregatorNode($"{stat} multipliers ({unit.Name})", null, unit.aggregator);
			public INode GetStatNode(Enum stat) => unit.ShortCircuit ? GetBaseStatNode(stat) : unit.Modifiers.StatsDetails.GetStatNode(stat);

			public BaseStatsRouter(BasicUnit unit)
			{
				this.unit = unit;
			}
		}

		[NonSerialized]
		private BaseStatsRouter statsRouter;

		private readonly IAggregator aggregator;

		/// <summary>
		/// Gets or sets the unit's name.
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// Gets or sets the unit's class.
		/// </summary>
		public virtual string Class { get; set; }

		/// <summary>
		/// Gets or sets the unit's level.
		/// </summary>
		public virtual int Level { get; set; }

		/// <summary>
		/// Gets a representation of the unit's stats.
		/// </summary>
		public virtual IStats Stats => ShortCircuit ? StatsDetails.Base : StatsDetails.Final;

		/// <summary>
		/// Gets a detailed breakdown of the unit's stats.
		/// </summary>
		public virtual IStatsPackage StatsDetails => statsRouter;

		/// <summary>
		/// Gets an editable version of the unit's base stats.
		/// </summary>
		public IEditableStats BaseStats { get; protected set; }

		/// <summary>
		/// Gets the unit's modifiers.
		/// </summary>
		public IUnitModifierStack Modifiers { get; protected set; }

		protected bool ShortCircuit => Modifiers.StatsDetails == StatsDetails;

		/// <summary>
		/// Set internal object references
		/// </summary>
		protected virtual void Link()
		{
			statsRouter = new BaseStatsRouter(this);

			// This will initiate the modifier rebinding calls
			Modifiers.Bind(this);
		}

		[OnDeserialized]
		private void Rebind(StreamingContext context)
		{
			// Reset object references after deserialization
			Link();
		}
		
		protected BasicUnit(IEditableStats baseStats, IAggregator aggregator)
		{
			this.aggregator = aggregator;
			BaseStats = baseStats;

			Modifiers = new UnitModifierStack();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicUnit"/> class with no properties set.
		/// </summary>
		public static BasicUnit Create()
			=> Create(StatsOperations.Default);

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicUnit"/> class with the specified <see cref="IAggregator"/>.
		/// </summary>
		/// <param name="aggregator">The <see cref="IAggregator"/> that defines the seed for aggregating stat multipliers.</param>
		public static BasicUnit Create(IAggregator aggregator)
			=> Create(new StatsMap(), aggregator);

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicUnit"/> class with the specified <see cref="IEditableStats"/> base stats and <see cref="IAggregator"/>.
		/// </summary>
		/// <param name="baseStats">The unit's base stats.</param>
		/// <param name="aggregator">The <see cref="IAggregator"/> that defines the seed for aggregating stat multipliers.</param>
		public static BasicUnit Create(IEditableStats baseStats, IAggregator aggregator)
		{
			BasicUnit unit = new BasicUnit(baseStats, aggregator);
			unit.Link();
			return unit;
		}
	}
}
