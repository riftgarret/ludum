using Redninja.Components.Combat;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	public delegate IBattleOperation OperationProvider(IUnitModel entity, ITargetResolver target, ISkillOperationParameters args);

	public static class OperationProviders
	{
		public static OperationProvider Damage { get; } = (e, t, args) => new DamageOperation(e, t, args);
	}
}
