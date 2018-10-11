using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Decisions;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.AI
{
	public class AIRuleSet
	{
		// TODO add a default rule for standard attack to all rules, talk to rice about best course
		private List<AIRule> Rules { get; } = new List<AIRule>();

		public void AddRule(AIRule rule) => Rules.Add(rule);

		public IBattleAction ResolveAction(IBattleEntity source, IBattleEntityManager bem) 
		{
			// TODO filter by refresh time so we dont repick the same skill by refresh requirement
			// find rules triggers
			IEnumerable<AIRule> validRules = Rules.Where(rule => rule.IsValidTriggerConditions(source, bem));				

			// assign pool
			WeightedPool<AIRule> weightedPool = new WeightedPool<AIRule>();
			validRules.ToList().ForEach(x => weightedPool.Add(x, x.Weight));

			SkillSelectionMeta skillMeta = DecisionHelper.GetAvailableSkills(source);
			
			// cycle through rules until we find one we can assign
			while(weightedPool.Count() > 0)
			{
				// pick weighted rule
				AIRule rule = weightedPool.Random();

				// filter out what skills this rule uses
				IEnumerable<ICombatSkill> availableSkills = rule.GetAssignableSkills(skillMeta);

				// attempt to find targets for first valid skill
				foreach(ICombatSkill skill in availableSkills)
				{
					// look for available targets
					ISkillTargetingManager targetMeta = DecisionHelper.GetTargetingManager(source, bem, skill);

					while (rule.TryFindTarget(targetMeta, source, bem, out ISelectedTarget target))
					{
						targetMeta.SelectTarget(target);

						if (targetMeta.Ready)
							return targetMeta.GetAction();
					}					
				}

				// no targets found for any skill, prune and research
				weightedPool.Remove(rule);
			}

			throw new InvalidOperationException("We couldnt find any rules to use, we should have implemented attack for all!");
		}
	}
}
