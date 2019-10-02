using System;
using System.Collections.Generic;

namespace Davfalcon.Nodes
{
	public class ResolverNode : NodeEnumerableBase, IResolverNode
	{
		private readonly IStatsOperations resolver;
		private readonly INode[] nodes = new INode[3];

		public INode Base => nodes[0];
		public INode Addend => nodes[1];
		public INode Multiplier => nodes[2];

		public ResolverNode(string name, INode baseValue, INode addend, INode multiplier, IStatsOperations resolver)
		{
			this.resolver = resolver ?? throw new ArgumentNullException();
			Name = name;
			nodes[0] = baseValue;
			nodes[1] = addend;
			nodes[2] = multiplier;

			Value = resolver.Calculate(Base.Value, Addend.Value, Multiplier.Value);
		}

		protected override IEnumerator<INode> GetEnumerator()
			=> (nodes as IEnumerable<INode>).GetEnumerator();

		protected override string GetTypeName() => "Resolver";
	}
}
