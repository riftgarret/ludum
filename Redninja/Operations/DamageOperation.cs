using System;
using Davfalcon.Revelator;

namespace Redninja.Operations
{
	public class DamageOperation : BattleOperationBase
	{
		private readonly IBattleEntity unit;
		private readonly IBattleEntity target;
		private readonly IDamageSource source;

		public DamageOperation(float time, IBattleEntity unit, IBattleEntity target, IDamageSource source)
			: base(time)
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.source = source ?? throw new ArgumentNullException(nameof(source));
		}

		public override void Execute(IBattleEntityManager manager, ICombatExecutor combatExecutor)
		{
			combatExecutor.DealDamage(unit, target, source);
		}
	}
}
