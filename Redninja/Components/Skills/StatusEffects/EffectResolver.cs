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
		private readonly ITargetPattern pattern;
		private readonly ISkillOperationParameters args;

		public event Action<float, IBattleOperation> BattleOperationReady;

		void IEffect.Resolve(IUnit unit, IUnit target, params object[] args)
		{
			IUnitModel entity = unit as IUnitModel;
			IUnitModel targetEntity = target as IUnitModel;
			ITargetResolver targetResolver =
				pattern == null
				? new SelectedTarget(rule, targetEntity)
				: new SelectedTargetPattern(rule, pattern, targetEntity.Team, targetEntity.Position) as ITargetResolver;
			BattleOperationReady.Invoke((float)args[0], operationProvider(entity, targetResolver, this.args));
		}

		public EffectResolver(ITargetingRule rule, ITargetPattern pattern, OperationProvider operationProvider, ISkillOperationParameters args)
		{
			this.rule = rule;
			this.pattern = pattern;
			this.operationProvider = operationProvider;
			this.args = args;
		}
	}
}
