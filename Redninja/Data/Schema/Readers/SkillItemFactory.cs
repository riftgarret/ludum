using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema.Readers
{
	internal class SkillItemFactory : IDataItemFactory<ISkill>
	{
		public ISkill CreateInstance(string dataId, ISchemaStore store)
		{
			CombatSkillSchema item = store.GetSchema<CombatSkillSchema>(dataId);

			return CombatSkill.Build(item.Name, ParseHelper.ParseStatsParams(item.DefaultStats), b =>
			{
				b.SetActionTime(ParseHelper.ParseActionTime(item.Time));
				item.TargetingSets.ForEach(ts => 
				b.AddTargetingSet(store.SingleInstance<ITargetingRule>(ts.TargetingRuleId),
					set =>
					{
						foreach (CombatRoundSchema combatRound in ts.CombatRounds)
						{
							set.AddCombatRound(
								combatRound.ExecutionStart,
								combatRound.Pattern != null ? ParseHelper.ParsePattern(combatRound.Pattern) : null,
								ParseHelper.ParseOperationProvider(combatRound.OperationProviderName),
								ParseHelper.ParseStatsParams(combatRound.Stats ?? item.DefaultStats));
						}
						return set;
					}));
				return b;
			});			
		}
	}
}
