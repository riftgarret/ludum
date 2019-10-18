using System;
using Davfalcon.Nodes;

namespace Davfalcon.Stats
{
	public interface IStatNode : INode<int>
	{
		string Name { get; }
		int Base { get; }
		INode<int> GetModification(Enum type);
	}
}
