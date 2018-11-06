using System;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;
using ParamsFunc = Redninja.Components.Skills.SkillOperationParameters.Builder.Func;

namespace Redninja.Components.Skills.StatusEffects
{
	public class StatusEffect : Buff, IStatusEffect
	{
		private IClock clock;
		private float startTime;
		private float nextTime;

		public float TimeDuration { get; private set; }
		public float TimeInterval { get; private set; }
		public float RemainingTime => TimeDuration + startTime - clock.Time;

		public IUnitModel EffectTarget { get; set; }

		public event Action<float, IBattleOperation> BattleOperationReady;
		public event Action<IStatusEffect> Expired;

		private void NextInterval() => nextTime += TimeInterval;

		// This replaces IBuff.Tick(), which was originally meant to work with turn counts
		protected virtual void OnTick(float timeDelta)
		{
			if (RemainingTime >= 0 && clock.Time >= nextTime)
			{
				TriggerEffects(nextTime);
				NextInterval();
			}

			if (RemainingTime <= 0)
				Expired?.Invoke(this);
		}

		protected virtual void TriggerEffects(float time)
		{
			foreach (IEffect effect in Effects)
			{
				effect.Resolve(Owner, EffectTarget, time);
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

		private StatusEffect() { }

		new public class Builder : BuilderBase<StatusEffect, IStatusEffect, Builder>
		{
			private readonly string name;

			public Builder(string name)
			{
				this.name = name;
				Reset();
			}

			public override Builder Reset() => Reset(new StatusEffect() { Name = name, TimeInterval = 1 });

			public Builder SetDuration(float duration) => Self(b => b.TimeDuration = duration);
			public Builder SetEffectInterval(float interval) => Self(b => b.TimeInterval = interval);

			public Builder IsDebuff() => Self(b => b.IsDebuff = true);

			public Builder SetStatAddition(Enum stat, int value) => Self(b => b.Additions[stat] = value);
			public Builder SetStatMultiplier(Enum stat, int value) => Self(b => b.Multipliers[stat] = value);

			private Builder AddEffect(EffectResolver effect) => Self(b =>
			{
				effect.BattleOperationReady += (f, o) => b.BattleOperationReady?.Invoke(f, o);
				b.Effects.Add(effect);
			});

			// Schema loading
			public Builder AddEffect(OperationProvider operationProvider, ISkillOperationParameters args)
				=> AddEffect(new EffectResolver(operationProvider, args));

			public Builder AddEffect(ITargetingRule rule, OperationProvider operationProvider, ISkillOperationParameters args)
				=> AddEffect(new EffectResolver(rule, operationProvider, args));

			public Builder AddEffect(ITargetPattern pattern, TargetTeam team, TargetCondition condition, OperationProvider operationProvider, ISkillOperationParameters args)
				=> AddEffect(new TargetingRule(pattern, team, condition), operationProvider, args);

			// Functional creation
			public Builder AddEffect(OperationProvider operationProvider, ParamsFunc args)
				=> AddEffect(operationProvider, args(new SkillOperationParameters.Builder(name)).Build());

			public Builder AddEffect(ITargetingRule rule, OperationProvider operationProvider, ParamsFunc args)
				=> AddEffect(rule, operationProvider, args(new SkillOperationParameters.Builder(name)).Build());

			public Builder AddEffect(ITargetPattern pattern, TargetTeam team, TargetCondition condition, OperationProvider operationProvider, ParamsFunc args)
				=> AddEffect(pattern, team, condition, operationProvider, args(new SkillOperationParameters.Builder(name)).Build());
		}

		public static IStatusEffect Build(string name, Builder.Func builderFunc) => builderFunc(new Builder(name)).Build();
	}
}
