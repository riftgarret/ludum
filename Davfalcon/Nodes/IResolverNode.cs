namespace Davfalcon.Nodes
{
	public interface IResolverNode : INode
	{
		INode Base { get; }
		INode Addend { get; }
		INode Multiplier { get; }
	}
}
