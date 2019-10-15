using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Logging;
using static Redninja.Components.Decisions.AI.AIActionDecisionResult;

namespace Redninja.Components.Decisions.AI
{
	/// <summary>
	/// Logic for executing AI Behavior and rules. Note due the complexity of this class
	/// it needs to be very testable. Many methods are marked as virtual to allow partial
	/// class mock and slying to control the execution environment during tests.
	/// </summary>
	internal class AIExecutor
	{
		private readonly AIRuleSet behavior;
		private readonly IAIRuleTracker history;
		private readonly IBattleEntity source;
		private IBattleModel battleModel => context.BattleModel;
		private IActionContextProvider acp;
		private readonly IBattleContext context;

		private AIActionDecisionResult.Tracker tracker;

		public AIExecutor(
			IBattleContext context,
			IBattleEntity source, 
			AIRuleSet behavior, 			
			IAIRuleTracker historyState)
		{
			this.behavior = behavior;
			this.source = source;
			this.history = historyState;
			this.context = context;
			this.acp = source.ActionContextProvider;
		}				

		public AIActionDecisionResult ResolveAction()
		{
			// DEBUG rule name -> in pool
			tracker = new AIActionDecisionResult.Tracker();
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
				var result = weightedPool.RandomResult();
				tracker.RecordWeighedPoolResult(weightedPool, result);
				IAIRule rule = result.Item1;
				
				if (TryGetAction(rule, out IBattleAction action))
				{
					LogResult(debugRuleMeta, rule, action);
					history.AddEntry(rule, action);
					tracker[rule].RuleResolved = true;
					return tracker.BuildResult(action);
				}

				tracker[rule].RuleResolved = false;

				// no targets found for any skill, prune and research
				weightedPool.Remove(rule);
			}
			LogResult(debugRuleMeta, null, null);			
			return tracker.BuildResult(new WaitAction(2));
		}


		private void LogResult(Dictionary<string, bool> ruleMeta, IAIRule resolvedRule, IBattleAction resultAction)
		{
			string ruleStr = resolvedRule != null ? resolvedRule.RuleName : "NONE";
			string actionStr = resultAction != null ? resultAction.Name : "NONE";
			RLog.D(this, $"Behavior RuleSet:\n\tresultRule:{ruleStr}\n\tresultAction: {actionStr}\n\tValid Rules: {string.Join(";", ruleMeta)}");
		}

		#region generic rule handling

		internal virtual IEnumerable<IAIRule> GetValidRules()
			=> behavior.Rules.Where(rule => {
				bool isReady = history.IsRuleReady(rule);
				bool isValidTrigger = IsValidTriggerConditions(rule);
				tracker[rule].IsReady = isReady;
				tracker[rule].IsValidTrigger = isValidTrigger;
				return isReady && isValidTrigger;
			});		


		internal virtual bool TryGetAction(IAIRule rule, out IBattleAction action)
		{
			tracker[rule].RuleEvaluated = true;
			if (rule is IAISkillRule) return TryGetSkillAction(rule as IAISkillRule, out action);			
			action = null;
			return false;
		}


		internal virtual bool IsValidTriggerConditions(IAIRule rule)
		{
			foreach (var trigger in rule.TriggerConditions)
			{
				var validEntities = FilterByType(trigger.Item1);				

				bool foundValid = validEntities.Any(trigger.Item2.IsValid);

				if (!foundValid) return false;
			}
			return true;
		}

		internal virtual IEnumerable<IBattleEntity> FilterByType(TargetTeam type)
			=> AIHelper.FilterByType(type, source, battleModel);

		#endregion
		#region skill action
		internal virtual bool TryGetSkillAction(IAISkillRule rule, out IBattleAction action)
		{
			var ruleEval = tracker[(IAIRule)rule];
			rule.SkillAssignments
				.Select(x => x.Item2)
				.ForEach(skill => ruleEval[skill].HasSkill = false);

			IActionContext skillMeta = acp.GetActionContext();

			// filter out what skills this rule uses
			IEnumerable<ISkill> availableSkills = FilterAssignableSkills(rule, skillMeta);			
			availableSkills.ForEach(skill => ruleEval[skill].HasSkill = true);
			
			// attempt to find targets for first valid skill
			foreach (ISkill skill in availableSkills)
			{
				var skillEval = ruleEval[skill];
				skillEval.SkillEvaluated = true;

				// look for available targets
				ITargetingContext targetMeta = acp.GetTargetingContext(skill);				
				foreach(ITargetSpec spec in targetMeta.TargetSpecs)
				{
					// currently we only support targeting entities.
					if (!(spec is EntityTargetSpec)) {
						throw new NotImplementedException("Missing impl for non-entity target spec");
					}

					if (!TryFindSkillTarget(skillEval, rule, (EntityTargetSpec)spec)) break;

					if(targetMeta.IsReady)
					{
						action = targetMeta.GetAction();
						skillEval.SkillResolved = true;
						return true;
					}
				}				

				skillEval.SkillResolved = false;
			}
			action = null;
			return false;
		}

		internal virtual IEnumerable<ISkill> FilterAssignableSkills(IAISkillRule rule, IActionContext meta)
			=> meta.Skills.Intersect(rule.SkillAssignments.Select(x => x.Item2));


		internal virtual bool TryFindSkillTarget(SkillEval skillEval, IAISkillRule rule, EntityTargetSpec targetSpec)
		{
			// filter targets
			IEnumerable<IBattleEntity> filteredTargets = GetValidSkillTargets(skillEval, rule, targetSpec.TargetRule);

			if (filteredTargets.Count() == 0) return false; // didnt find any valid targets			

			// TODO track meta for selecting target

			// select best target
			IAITargetPriority targetPriority = rule.SkillAssignments.FirstOrDefault(x => x.Item2 == targetSpec.Skill).Item1;
			IBattleEntity selectedTarget = targetPriority.GetBestTarget(filteredTargets);

			targetSpec.SelectTarget(selectedTarget);			
			return true;
		}

		internal IEnumerable<IBattleEntity> GetValidSkillTargets(SkillEval skillEval, IAISkillRule rule, ITargetingRule targetingRule)
		{
			// initialize all entities to be recorded
			IEnumerable<IBattleEntity> allEntities = battleModel.Entities;
			allEntities.ForEach(x => skillEval[x].IsValidType = false);

			// first filter by TargetType
			IEnumerable<IBattleEntity> leftoverTargets = FilterByType(rule.TargetType);
			leftoverTargets.ForEach(x => skillEval[x].IsValidType = true);

			// filter by skill rule
			leftoverTargets = leftoverTargets.Where(ex => targetingRule.IsValidTarget(ex, source));
			leftoverTargets.ForEach(x => skillEval[x].IsValidTarget = true);

			// filter by filter conditions (exclude by finding first condition that fails)
			leftoverTargets = leftoverTargets.Where(ex => rule.FilterConditions.All(cond => cond.IsValid(ex)));
			leftoverTargets.ForEach(x => skillEval[x].IsValidConditions = true);

			return leftoverTargets;
		}
		#endregion
		#region Attack Rule

		// TODO refactor to get default skill if none.
		internal virtual bool TryGetDefaultSkill(IAIAttackRule rule, out IBattleAction action)
		{
			IActionContext skillMeta = acp.GetActionContext();

			ITargetingContext targetMeta = acp.GetTargetingContext(skillMeta.Skills.First()); // first temp to compile
			
			IEnumerable<IBattleEntity> leftoverTargets = FilterByType(TargetTeam.Enemy);			
			leftoverTargets = leftoverTargets.Where(ex => TargetConditions.MustBeAlive(ex, source));

			if(leftoverTargets.Count() == 0)
			{
				action = null;
				return false;
			}

			IBattleEntity entityTarget = rule.TargetPriority.GetBestTarget(leftoverTargets);
			//ISelectedTarget selectedTarget = targetMeta.GetSelectedTarget(entityTarget);

			//targetMeta.SelectTarget(selectedTarget);
			//action = targetMeta.GetAction();
			action = null;
			return true;
		}

		#endregion
	}
}
