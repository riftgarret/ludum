using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	public class ActionContextProvider : IActionContextProvider
	{
		private IBattleEntity unit;
		private IBattleContext context;

		public ActionContextProvider(IBattleContext context, IBattleEntity unit)
		{
			this.unit = unit;
			this.context = context;
		}

		public IActionContext GetActionContext()
			=> new SkillSelectionContext(unit, context.SystemProvider.GetSkillProvider(unit));

		public IMovementContext GetMovementContext()
			=> new MovementContext(unit, context.BattleModel);

		public ITargetingContext GetTargetingContext(ISkill skill)
			=> new TargetingContext(unit, skill, context.BattleModel);
	}
}
