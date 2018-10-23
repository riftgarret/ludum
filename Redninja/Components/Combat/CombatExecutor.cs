using System;
using Davfalcon.Builders;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.Events;

namespace Redninja.Components.Combat
{
	internal class CombatExecutor : ICombatExecutor
	{
		private readonly ICombatNodeResolver resolver;

		public event Action<IUnitModel, Coordinate> EntityMoving;
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

		public void InitializeEntity(IUnitModel entity) => resolver.Initialize(entity.Character);
		public void CleanupEntity(IUnitModel entity) => resolver.Cleanup(entity.Character);

		public void MoveEntity(IUnitModel entity, int newRow, int newCol)
		{
			UnitPosition originalPosition = entity.Position;
			EntityMoving.Invoke(entity, new Coordinate(newRow, newCol));
			BattleEventOccurred?.Invoke(new MovementEvent(entity, entity.Position, originalPosition));
		}

		public void MoveEntity(IUnitModel entity, UnitPosition newPosition)
			=> MoveEntity(entity, newPosition.Row, newPosition.Column);

		public IDamageNode GetRawDamage(IUnitModel attacker, IDamageSource source)
			=> resolver.GetDamageNode(attacker.Character, source);

		public IDefenseNode GetDamage(IUnitModel attacker, IUnitModel defender, IDamageSource source)
			=> GetDamage(defender, GetRawDamage(attacker, source));

		public IDefenseNode GetDamage(IUnitModel defender, IDamageNode incomingDamage)
			=> resolver.GetDefenseNode(defender.Character, incomingDamage);

		public IDefenseNode DealDamage(IUnitModel attacker, IUnitModel defender, IDamageSource source)
		{
			IDefenseNode damage = GetDamage(attacker, defender, source);
			BattleEventOccurred?.Invoke(new DamageEvent(defender, damage, resolver.ApplyDamage(damage)));
			return damage;
		}
	}
}
