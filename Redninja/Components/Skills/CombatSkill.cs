using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Actions;
using Redninja.Components.Combat;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	public class CombatSkill : ISkill
	{
		public string Name { get; set; }
		public ActionTime Time { get; set; }

		public List<SkillTargetingSet> Targets { get; } = new List<SkillTargetingSet>();
		IReadOnlyList<SkillTargetingSet> ISkill.Targets => Targets;

		public IBattleAction GetAction(IBattleEntity entity, List<ISelectedTarget> targets)
		{
			List<IBattleOperation> ops = new List<IBattleOperation>();
			for (int i = 0; i < Targets.Count; i++)
			{
				ops.AddRange(Targets[i].OpDefinitions.Select(opDef => opDef.CreateOperation(entity, targets[i])));				
			}
			return new SkillAction(entity, this, ops);
		}		
	}
}
