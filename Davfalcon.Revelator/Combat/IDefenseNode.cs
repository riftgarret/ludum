using Davfalcon.Nodes;

namespace Davfalcon.Revelator.Combat
{
	public interface IDefenseNode : IResolverNode
	{
		IUnit Defender { get; }
		IDamageNode IncomingDamage { get; }
	}
}
