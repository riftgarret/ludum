using System;

namespace Davfalcon.Nodes
{
	public interface IStatNode<T> : INode
	{
		Enum Stat { get; }
		T Source { get; }
	}
}
