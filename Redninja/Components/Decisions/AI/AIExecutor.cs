﻿using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Logging;

namespace Redninja.Components.Decisions.AI
{
	/// <summary>
	/// Logic for executing AI Behavior and rules. Note due the complexity of this class
	/// it needs to be very testable. Many methods are marked as virtual to allow partial
	/// class mock and slying to control the execution environment during tests.
	/// </summary>
	internal class AIExecutor
	{
		private readonly AIBehavior behavior;
		private readonly IAIHistoryState history;
		private readonly IUnitModel source;
		private readonly IDecisionHelper decisionHelper;
		private readonly IBattleModel battleModel;

		public AIExecutor(IUnitModel source, 
			AIBehavior behavior, 
			IDecisionHelper decisionHelper, 
			IAIHistoryState historyState)
		{
			this.behavior = behavior;
			this.source = source;
			this.decisionHelper = decisionHelper;
			this.history = historyState;
			this.battleModel = decisionHelper.BattleModel;
		}				

		public IBattleAction ResolveAction()
		{
			// DEBUG rule name -> in pool
			var debugRuleMeta = new Dictionary<string, bool>();
			behavior.Rules.ForEach(x => debugRuleMeta[x.RuleName] = false);

			// find rules triggers
			IEnumerable<IAIRule> validRules = GetValidRules();

			// assign pool
			WeightedPool<IAIRule> weightedPool = new WeightedPool<IAIRule>();
			foreach (var rule in validRules)
			{
				weightedPool.Add(rule, rule.Weight);
				debugRuleMeta[rule.RuleName] = true;
			}

			// cycle through rules until we find one we can assign
			while (weightedPool.Count() > 0)
			{
				// pick weighted rule
				IAIRule rule = weightedPool.Random();
				
				if (TryGetAction(rule, out IBattleAction action))
				{
					LogResult(debugRuleMeta, rule, action);
					history.AddEntry(rule, action);
					return action;
				}

				// no targets found for any skill, prune and research
				weightedPool.Remove(rule);
			}
			LogResult(debugRuleMeta, null, null);
			return null;
		}

		private void LogResult(Dictionary<string, bool> ruleMeta, IAIRule resolvedRule, IBattleAction resultAction)
		{
			string ruleStr = resolvedRule != null ? resolvedRule.RuleName : "NONE";
			string actionStr = resultAction != null ? resultAction.GetType().ToString() : "NONE";
			RLog.D(this, $"RuleSet - resultRule:{ruleStr} resultAction: {actionStr}\n\t{ruleMeta}");
		}

		#region generic rule handling

		internal virtual IEnumerable<IAIRule> GetValidRules()
			=> behavior.Rules.Where(rule => history.IsRuleReady(rule) && IsValidTriggerConditions(rule));		

		internal virtual bool TryGetAction(IAIRule rule, out IBattleAction action)
		{
			if (rule is IAISkillRule) return TryGetSkillAction(rule as IAISkillRule, out action);

			action = null;
			return false;
		}
		
		internal virtual bool IsValidTriggerConditions(IAIRule rule)
		{
			foreach (var trigger in rule.TriggerConditions)
			{
				var validEntities = FilterByType(trigger.Item1);

				if (validEntities.Count() == 0) return false; // couldnt find any targets to test triggers

				bool foundValid = null != validEntities.FirstOrDefault(ex => trigger.Item2.IsValid(ex));

				if (!foundValid) return false;
			}
			return true;
		}

		internal virtual IEnumerable<IUnitModel> FilterByType(TargetTeam type)
			=> AIHelper.FilterByType(type, source, battleModel);

		#endregion
		#region skill action
		internal virtual bool TryGetSkillAction(IAISkillRule rule, out IBattleAction action)
		{
			IActionsContext skillMeta = decisionHelper.GetActionsContext(source);

			// filter out what skills this rule uses
			IEnumerable<ISkill> availableSkills = FilterAssignableSkills(rule, skillMeta);

			// attempt to find targets for first valid skill
			foreach (ISkill skill in availableSkills)
			{
				// look for available targets
				ITargetingContext targetMeta = decisionHelper.GetTargetingContext(source, skill);

				// found!				
				while (TryFindTarget(rule, targetMeta, out ISelectedTarget selectedTarget))
				{
					targetMeta.SelectTarget(selectedTarget);

					if (targetMeta.Ready)
					{
						action = targetMeta.GetAction();
						return true;
					}
				}
			}
			action = null;
			return false;
		}

		internal virtual IEnumerable<ISkill> FilterAssignableSkills(IAISkillRule rule, IActionsContext meta)
			=> meta.Skills.Intersect(rule.SkillAssignments.Select(x => x.Item2));


		internal virtual bool TryFindTarget(IAISkillRule rule, ITargetingContext meta, out ISelectedTarget selectedTarget)
		{
			// filter targets
			IEnumerable<IUnitModel> filteredTargets = GetValidTargets(rule, meta.TargetingRule);

			if (filteredTargets.Count() == 0)
			{
				selectedTarget = null;
				return false; // didnt find any valid targets
			}
			
			// select best target
			IAITargetPriority targetPriority = rule.SkillAssignments.FirstOrDefault(x => x.Item2 == meta.Skill).Item1;
			IUnitModel entityTarget = targetPriority.GetBestTarget(filteredTargets);

			// convert into ISelectedTarget
			selectedTarget = meta.GetSelectedTarget(entityTarget);
			return true;
		}

		internal IEnumerable<IUnitModel> GetValidTargets(IAISkillRule rule, ITargetingRule targetingRule)
		{
			// first filter by TargetType
			IEnumerable<IUnitModel> leftoverTargets = FilterByType(rule.TargetType);

			// filter by skill rule
			leftoverTargets = leftoverTargets.Where(ex => targetingRule.IsValidTarget(ex, source));

			// filter by filter conditions (exclude by finding first condition that fails)
			leftoverTargets = leftoverTargets.Where(ex => rule.FilterConditions.FirstOrDefault(cond => !cond.IsValid(ex)) == null);
			return leftoverTargets;
		}
		#endregion
	}
}