using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	public class ActionContextProvider : IActionContextProvider
	{
		private IUnitModel unit;
		private IBattleContext context;

		public ActionContextProvider(IBattleContext context, IUnitModel unit)
		{
			this.unit = unit;
			this.context = context;
		}

		public IActionContext GetActionContext()
			=> new SkillSelectionContext(unit, context.SystemProvider.GetSkillProvider(unit));

		public IMovementContext GetMovementContext()
			=> new MovementContext(unit, context.BattleModel);

		public ITargetingContext GetTargetingContext(ISkill skill)
			=> new SkillTargetingContext(unit, skill, context.BattleModel);
	}
}
