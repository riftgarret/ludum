using System;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Combat
{
	internal class CombatExecutor : ICombatExecutor
	{
		private readonly ICombatResolver resolver;

		// I want to remove this
		public event Action<IUnitModel, Coordinate> EntityMoving;
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

		public void InitializeEntity(IUnitModel entity) => resolver.Initialize(entity);
		public void CleanupEntity(IUnitModel entity) => resolver.Cleanup(entity);

		public void MoveEntity(IUnitModel entity, int newRow, int newCol)
		{
			UnitPosition originalPosition = entity.Position;
			EntityMoving.Invoke(entity, new Coordinate(newRow, newCol));
			BattleEventOccurred?.Invoke(new MovementEvent(entity, entity.Position, originalPosition));
		}

		public void MoveEntity(IUnitModel entity, UnitPosition newPosition)
			=> MoveEntity(entity, newPosition.Row, newPosition.Column);

		public void ApplyStatusEffect(IUnitModel source, IUnitModel target, IBuff effect)
		{
			resolver.ApplyBuff(target, effect);
			BattleEventOccurred?.Invoke(new StatusEffectEvent(source, target, effect));
		}

		public void RemoveStatusEffect(IUnitModel entity, IBuff effect)
		{
			resolver.RemoveBuff(entity, effect);
		}

		public IDamageNode GetRawDamage(IUnitModel attacker, IDamageSource source)
			=> resolver.GetDamageNode(attacker, source);

		public IDefenseNode GetDamage(IUnitModel attacker, IUnitModel defender, IDamageSource source)
			=> GetDamage(defender, GetRawDamage(attacker, source));

		public IDefenseNode GetDamage(IUnitModel defender, IDamageNode incomingDamage)
			=> resolver.GetDefenseNode(defender, incomingDamage);

		public void DealDamage(IUnitModel attacker, IUnitModel defender, IDamageSource source)
		{
			IDefenseNode damage = GetDamage(attacker, defender, source);
			BattleEventOccurred?.Invoke(new DamageEvent(attacker, defender, damage, resolver.ApplyDamage(damage)));
		}
	}
}
