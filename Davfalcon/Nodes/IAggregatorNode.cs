using System.Collections.Generic;

namespace Davfalcon.Nodes
{
	public interface IAggregatorNode : INode
	{
		IEnumerable<INode> Nodes { get; }
		IAggregatorNode Merge(INode node);
	}
}
