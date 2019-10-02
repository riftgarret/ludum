using Davfalcon.Nodes;

namespace Davfalcon.Revelator.Combat
{
	public interface IDamageNode : IResolverNode
	{
		IUnit Unit { get; }
		IDamageSource Source { get; }
	}
}
