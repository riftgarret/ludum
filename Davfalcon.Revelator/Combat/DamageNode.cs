using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Nodes;

namespace Davfalcon.Revelator.Combat
{
	internal class DamageNode : NodeEnumerableBase, IDamageNode
	{
		private readonly IList<INode> nodes = new List<INode>(3);

		public IUnit Unit { get; }
		public IDamageSource Source { get; }
		public INode Base { get; }
		public INode Addend { get; }
		public INode Multiplier { get; }

		public DamageNode(IDamageSource source, IUnit unit, ICombatResolver resolver)
		{
			ICombatOperations operations = resolver?.Operations ?? throw new ArgumentNullException();

			Unit = unit ?? throw new ArgumentNullException();
			Source = source ?? throw new ArgumentNullException();

			IEnumerable<Enum> scalingStats = resolver.GetDamageScalingStats(source.DamageTypes);

			Base = new ConstantNode("Base damage", Source.BaseDamage);
			Addend = Source.BonusDamageStat != null ? Unit.StatsDetails.GetStatNode(Source.BonusDamageStat) : null;

			if (scalingStats.Count() > 1)
			{
				List<INode> nodes = new List<INode>();
				foreach (Enum stat in scalingStats)
				{
					nodes.Add(Unit.StatsDetails.GetStatNode(stat));
				}
				Multiplier = new AggregatorNode("Damage scaling", nodes, operations);
			}
			else if (scalingStats.Count() == 1)
			{
				Multiplier = Unit.StatsDetails.GetStatNode(scalingStats.First());
			}

			nodes.Add(Base);
			if (Addend != null) nodes.Add(Addend);
			if (Multiplier != null) nodes.Add(Multiplier);

			Name = $"{Source.Name} ({Unit.Name}) {String.Join(" ", Source.DamageTypes.Select(type => $"[{type}]"))}";
			Value = operations.Calculate(Base.Value, Addend?.Value ?? 0, Multiplier?.Value ?? operations.AggregateSeed);
		}

		protected override IEnumerator<INode> GetEnumerator()
			=> (nodes as IEnumerable<INode>).GetEnumerator();
		
		protected override string GetTypeName() => "Damage";
	}
}
