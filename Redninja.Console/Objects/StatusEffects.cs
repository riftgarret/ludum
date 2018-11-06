using Davfalcon.Revelator.Borger;
using Redninja.Components.Skills;
using Redninja.Components.Skills.StatusEffects;

namespace Redninja.ConsoleDriver.Objects
{
	public static class StatusEffects
	{
		public static IStatusEffect Burn { get; } = StatusEffect.Build("Burn", b => b
			.AddEffect(OperationProviders.Damage, d => d
				.AddDamageType(DamageType.Magical)
				.AddDamageType(Element.Fire)
				.SetDamage(5))
			.SetDuration(5));
	}
}
