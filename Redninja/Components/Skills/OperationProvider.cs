using Davfalcon.Revelator;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	public delegate IBattleOperation OperationProvider(IBattleEntity entity, ITargetResolver target, ISkillOperationParameters args);

	public static class OperationProviders
	{
		public static OperationProvider Damage { get; } = (e, t, args) => new DamageOperation(e, t, args);
		public static OperationProvider GetStatusEffectProvider(IBuff buff) => (e, t, args) => new StatusEffectOperation(e, t, buff);
	}
}
