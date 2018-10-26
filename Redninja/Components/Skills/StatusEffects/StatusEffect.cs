using System;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;
using ParamsFunc = Redninja.Components.Skills.SkillOperationParameters.Builder.Func;

namespace Redninja.Components.Skills.StatusEffects
{
	internal class StatusEffect : Buff, IStatusEffect
	{
		private IClock clock;
		private float startTime;
		private float nextTime;

		public float TimeDuration { get; private set; }
		public float TimeInterval { get; private set; }
		public float RemainingTime => TimeDuration + startTime - clock.Time;

		public event Action<float, IBattleOperation> BattleOperationReady;

		private void NextInterval() => nextTime += TimeInterval;

		// This replaces IBuff.Tick(), which was originally meant to work with turn counts
		protected virtual void OnTick(float timeDelta)
		{
			if (Remaining <= 0 && clock.Time >= nextTime)
			{
				TriggerEffects(nextTime);
				NextInterval();
			}
		}

		protected virtual void TriggerEffects(float time)
		{
			foreach (IEffect effect in Effects)
			{
				effect.Resolve(Owner, Target as IUnit, time);
			}
		}

		public void SetClock(IClock clock)
		{
			UnsetClock();

			this.clock = clock;
			clock.Tick += OnTick;
			startTime = clock.Time;
			nextTime = startTime + TimeInterval;
		}

		private void UnsetClock()
		{
			if (clock != null)
			{
				clock.Tick -= OnTick;
				clock = null;
			}
		}

		public void Dispose()
		{
			UnsetClock();
		}

		new public class Builder : BuilderBase<StatusEffect, IStatusEffect, Builder>
		{
			private readonly string name;

			public Builder(string name)
			{
				this.name = name;
				Reset();
			}

			public override Builder Reset() => Reset(new StatusEffect() { Name = name });

			public Builder SetDuration(int duration) => Self(b => b.Duration = duration);

			public Builder IsDebuff() => Self(b => b.IsDebuff = true);

			public Builder SetStatAddition(Enum stat, int value) => Self(b => b.Additions[stat] = value);
			public Builder SetStatMultiplier(Enum stat, int value) => Self(b => b.Multipliers[stat] = value);

			private Builder AddEffect(EffectResolver effect) => Self(b =>
			{
				effect.BattleOperationReady += b.BattleOperationReady;
				b.Effects.Add(effect);
			});

			// Schema loading
			public Builder AddEffect(ITargetingRule rule, OperationProvider operationProvider, ISkillOperationParameters args)
				=> AddEffect(rule, null, operationProvider, args);

			public Builder AddEffect(ITargetingRule rule, ITargetPattern pattern, OperationProvider operationProvider, ISkillOperationParameters args)
				=> AddEffect(new EffectResolver(rule, pattern, operationProvider, args));

			// Functional creation
			public Builder AddEffect(ITargetingRule rule, OperationProvider operationProvider, ParamsFunc args)
				=> AddEffect(rule, operationProvider, args(new SkillOperationParameters.Builder(name)).Build());

			public Builder AddEffect(ITargetingRule rule, ITargetPattern pattern, OperationProvider operationProvider, ParamsFunc args)
				=> AddEffect(rule, pattern, operationProvider, args(new SkillOperationParameters.Builder(name)).Build());
		}
	}
}
