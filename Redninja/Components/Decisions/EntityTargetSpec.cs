using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions
{
	public class EntityTargetSpec : TargetSpec
	{			
		//internal EntityTargetSpec(
		//	IUnitModel source,
		//	ITargetingRule rule, 
		//	IBattleModel battleModel)
		//{
		//	this.source = source;
		//	this.targetRule = rule;
		//	this.battleModel = battleModel;
		//}
		

		public IEnumerable<IUnitModel> GetTargetableEntities()
		{
			switch (TargetRule.Team)
			{
				case TargetTeam.Ally:
					return battleModel.Entities.Where(e => e.Team == source.Team && IsValidTarget(e));
				case TargetTeam.Enemy:
					return battleModel.Entities.Where(e => e.Team != source.Team && IsValidTarget(e));
				case TargetTeam.Self:
					return IsValidTarget(source) ? Enumerable.Repeat(source, 1) : Enumerable.Empty<IUnitModel>();
				default: throw new InvalidOperationException();
			}
		}

		public bool IsValidTarget(IUnitModel unit) => TargetRule.IsValidTarget(source, unit);

		public void SelectTarget(IUnitModel target) => SelectedTarget = new SelectedTarget(TargetRule, target);
	}
}
