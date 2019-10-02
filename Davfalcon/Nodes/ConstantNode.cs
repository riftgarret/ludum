using System;

namespace Davfalcon.Nodes
{
	[Serializable]
	public class ConstantNode : NodeEnumerableBase
	{
		public ConstantNode(string name, int value)
		{
			Name = name;
			Value = value;
		}

		public ConstantNode(int value)
			: this("", value)
		{ }

		protected override string GetTypeName() => "Constant";

		public static ConstantNode One { get; } = new ConstantNode(1);
		public static ConstantNode Zero { get; } = new ConstantNode(0);
	}
}
