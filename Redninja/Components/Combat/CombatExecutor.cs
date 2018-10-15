using System;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.Events;

namespace Redninja.Components.Combat
{
	public class CombatExecutor : ICombatExecutor
	{
		private readonly ICombatNodeResolver resolver;

		public event Action<IBattleEvent> BattleEventOccurred;

		public CombatExecutor(ICombatNodeResolver combatResolver)
		{
			resolver = combatResolver;
		}

		public CombatExecutor(IBuilder<ICombatNodeResolver> builder)
			: this(builder.Build())
		{ }

		public CombatExecutor(Func<CombatResolver.Builder, CombatResolver.Builder> builderFunc)
			: this(builderFunc(new CombatResolver.Builder()))
		{ }

		public void InitializeEntity(IEntityModel entity)
		{
			resolver.Initialize(entity.Character);
		}

		public void MoveEntity(IEntityModel entity, int newRow, int newCol)
		{
			EntityPosition originalPosition = entity.Position;
			entity.MovePosition(newRow, newCol);
			BattleEventOccurred?.Invoke(new MovementEvent(entity, entity.Position, originalPosition));
		}

		public void MoveEntity(IEntityModel entity, EntityPosition newPosition)
			=> MoveEntity(entity, newPosition.Row, newPosition.Column);

		public IDamageNode GetRawDamage(IEntityModel attacker, IDamageSource source)
			=> resolver.GetDamageNode(attacker.Character, source);

		public IDefenseNode GetDamage(IEntityModel attacker, IEntityModel defender, IDamageSource source)
			=> GetDamage(defender, GetRawDamage(attacker, source));

		public IDefenseNode GetDamage(IEntityModel defender, IDamageNode incomingDamage)
			=> resolver.GetDefenseNode(defender.Character, incomingDamage);

		public IDefenseNode DealDamage(IEntityModel attacker, IEntityModel defender, IDamageSource source)
		{
			IDefenseNode damage = GetDamage(attacker, defender, source);
			BattleEventOccurred?.Invoke(new DamageEvent(defender, damage, resolver.ApplyDamage(damage)));
			return damage;
		}
	}
}
