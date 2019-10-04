using System;
using System.Collections.Generic;

namespace Davfalcon.Nodes
{
	public interface INode : INameable, IEnumerable<INode>
	{
		int Value { get; }
	}
}
