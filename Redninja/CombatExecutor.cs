using System;
using Davfalcon.Nodes;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.Events;

namespace Redninja
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

		public void InitializeEntity(IBattleEntity entity)
		{
			resolver.Initialize(entity.Character);
		}

		public void MoveEntity(IBattleEntity entity, int newRow, int newCol)
		{
			EntityPosition originalPosition = entity.Position;
			entity.MovePosition(newRow, newCol);
			BattleEventOccurred?.Invoke(new MovementEvent(entity, entity.Position, originalPosition));
		}

		public void MoveEntity(IBattleEntity entity, EntityPosition newPosition)
			=> MoveEntity(entity, newPosition.Row, newPosition.Column);

		public INode GetRawDamage(IBattleEntity attacker, IDamageSource source)
			=> resolver.GetDamageNode(attacker.Character, source);

		public INode GetDamage(IBattleEntity attacker, IBattleEntity defender, IDamageSource source)
		{
			throw new NotImplementedException();
		}

		public INode GetDamage(INode incomingDamage, IBattleEntity defender, IDamageSource source)
			=> resolver.GetDefenseNode(defender.Character, incomingDamage, source.DamageTypes);
	}
}
