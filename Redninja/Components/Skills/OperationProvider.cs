using Redninja.Components.Operations;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	public delegate IBattleOperation OperationProvider(IUnitModel entity, ITargetResolver target, ISkill skill);

	public static class OperationProviders
	{
		public static OperationProvider Damage { get; } = (e, t, s) => new DamageOperation(e, t, s);
	}
}
