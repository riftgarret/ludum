using System;
using Davfalcon.Revelator;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills.StatusEffects
{
	internal class EffectResolver : IEffect
	{
		private readonly OperationProvider operationProvider;
		private readonly ITargetingRule rule;
		private readonly ISkillOperationParameters args;

		public event Action<float, IBattleOperation> BattleOperationReady;

		void IEffect.Resolve(IUnit unit, IUnit target, params object[] args)
		{
			IBattleEntity entity = unit as IBattleEntity;
			IBattleEntity targetEntity = target as IBattleEntity;
			ITargetResolver targetResolver =
				rule == null
				? new StaticTarget(targetEntity)
				: new SelectedTargetPattern(rule, rule.Pattern, targetEntity.Team, targetEntity.Position) as ITargetResolver;
			BattleOperationReady?.Invoke((float)args[0], operationProvider(entity, targetResolver, this.args));
		}

		public EffectResolver(OperationProvider operationProvider, ISkillOperationParameters args)
			: this(null, operationProvider, args)
		{ }

		public EffectResolver(ITargetingRule rule, OperationProvider operationProvider, ISkillOperationParameters args)
		{
			this.rule = rule;
			this.operationProvider = operationProvider;
			this.args = args;
		}
	}
}
