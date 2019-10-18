using System;
using Davfalcon.Nodes;

namespace Davfalcon.Stats
{
	public struct StatModification
	{
		public Enum Type { get; }
		public INode<int> Modification { get; }
		public StatModification(Enum type, INode<int> modification) : this()
		{
			Type = type;
			Modification = modification;
		}
	}
}
