using System;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Combat
{
	public class CombatExecutor : ICombatExecutor
	{
		private readonly ICombatResolver resolver;

		// I want to remove this
		public event Action<IBattleEntity, Coordinate> EntityMoving;
		public event Action<ICombatEvent> BattleEventOccurred;

		public CombatExecutor(ICombatResolver combatResolver)
		{
			resolver = combatResolver;
		}

		public CombatExecutor(IBuilder<ICombatResolver> builder)
			: this(builder.Build())
		{ }

		public CombatExecutor(Func<CombatResolver.Builder, CombatResolver.Builder> builderFunc)
			: this(builderFunc(new CombatResolver.Builder()))
		{ }

		public void InitializeEntity(IBattleEntity entity) => resolver.Initialize(entity);
		public void CleanupEntity(IBattleEntity entity) => resolver.Cleanup(entity);

		public void MoveEntity(IBattleEntity entity, int newRow, int newCol)
		{
			UnitPosition originalPosition = entity.Position;
			EntityMoving.Invoke(entity, new Coordinate(newRow, newCol));
			BattleEventOccurred?.Invoke(new MovementEvent(entity, entity.Position, originalPosition));
		}

		public void MoveEntity(IBattleEntity entity, UnitPosition newPosition)
			=> MoveEntity(entity, newPosition.Row, newPosition.Column);

		public void ApplyStatusEffect(IBattleEntity source, IBattleEntity target, IBuff effect)
		{
			resolver.ApplyBuff(target, effect);
			BattleEventOccurred?.Invoke(new StatusEffectEvent(source, target, effect));
		}

		public void RemoveStatusEffect(IBattleEntity entity, IBuff effect)
		{
			resolver.RemoveBuff(entity, effect);
		}

		public IDamageNode GetRawDamage(IBattleEntity attacker, IDamageSource source)
			=> resolver.GetDamageNode(attacker, source);

		public IDefenseNode GetDamage(IBattleEntity attacker, IBattleEntity defender, IDamageSource source)
			=> GetDamage(defender, GetRawDamage(attacker, source));

		public IDefenseNode GetDamage(IBattleEntity defender, IDamageNode incomingDamage)
			=> resolver.GetDefenseNode(defender, incomingDamage);

		public void DealDamage(IBattleEntity attacker, IBattleEntity defender, IDamageSource source)
		{			
			IDefenseNode damage = GetDamage(attacker, defender, source);
			BattleEventOccurred?.Invoke(new DamageEvent(attacker, defender, damage, resolver.ApplyDamage(damage)));
		}
	}
}
