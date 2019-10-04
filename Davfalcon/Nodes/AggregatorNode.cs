using System;
using System.Collections.Generic;
using System.Linq;

namespace Davfalcon.Nodes
{
	public class AggregatorNode : NodeEnumerableBase, IAggregatorNode
	{
		private readonly IAggregator aggregator;

		public IEnumerable<INode> Nodes { get; }

		public AggregatorNode(string name, IEnumerable<INode> values, IAggregator aggregator)
		{
			this.aggregator = aggregator ?? throw new ArgumentNullException();
			Name = name;
			Nodes = values.ToNewReadOnlyCollectionSafe();

			Value = Nodes?.Select(node => node.Value).Aggregate(aggregator.AggregateSeed, aggregator.Aggregate) ?? aggregator.AggregateSeed;
		}

		public AggregatorNode(string name, IEnumerable<INode> values)
			: this(name, values, StatsOperations.Default)
		{ }

		public IAggregatorNode Merge(INode node)
			=> new AggregatorNode(Name, Nodes.Append(node), aggregator);

		public IAggregatorNode Merge(IAggregatorNode node)
			=> new AggregatorNode(Name, Nodes.Union(node.Nodes), aggregator);

		protected override IEnumerator<INode> GetEnumerator()
			=> Nodes.GetEnumerator();

		protected override string GetTypeName() => "Aggregator";
	}
}
