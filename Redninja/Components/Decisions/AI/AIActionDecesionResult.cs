using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Actions;
using Redninja.Components.Skills;

namespace Redninja.Components.Decisions.AI
{	
	/// <summary>
	/// Results from AI decision making.
	/// </summary>
	public class AIActionDecisionResult
	{
		//public List<Rule>
		public IEnumerable<RuleEval> RuleEvals { get; private set; }
		public IBattleAction Result { get; private set; }

		AIActionDecisionResult(IBattleAction result, IEnumerable<RuleEval> ruleEvals)
		{
			this.Result = result;
			this.RuleEvals = ruleEvals;
		}		

		/// <summary>
		/// Not a real builder but something to track adding all the changes.
		/// </summary>
		public class Tracker
		{
			private Dictionary<IAIRule, RuleEval> ruleMap = new Dictionary<IAIRule, RuleEval>();
			private List<WeightedRuleResult> weightedRuleResults = new List<WeightedRuleResult>();
			public RuleEval this[IAIRule rule]
			{
				get
				{
					if (!ruleMap.TryGetValue(rule, out RuleEval ruleEval))
					{
						ruleMap[rule] = ruleEval = new RuleEval
						{
							Rule = rule
						};						
					}
					return ruleEval;
				}
			}

			public void RecordWeighedPoolResult(WeightedPool<IAIRule> weightPool, Tuple<IAIRule, double> poolResult)
			{
				var result = new WeightedRuleResult
				{
					Value = poolResult.Item2
				};
				double cursor = 0;
				result.RuleValues = new List<Tuple<IAIRule, double>>();
				foreach(var item in weightPool.WeightedItems)
				{
					cursor += item.Item2;
					result.RuleValues.Add(Tuple.Create(item.Item1, cursor));
				}				

				weightedRuleResults.Add(result);
			}

			public AIActionDecisionResult BuildResult(IBattleAction action) => new AIActionDecisionResult(action, ruleMap.Values);
		}

		public class WeightedRuleResult
		{
			public double Value { get; set; }
			public List<Tuple<IAIRule, double>> RuleValues { get; set; }
			public IAIRule SelectedRule { get; set; }
		}

		public class RuleEval
		{
			public IAIRule Rule { get; set; }
			public bool IsReady { get; set; }
			public bool IsValidTrigger { get; set; }			
			public bool IsValid() => IsReady && IsValidTrigger;
			public bool RuleEvaluated { get; set; }
			public bool RuleResolved { get; set; }
			private Dictionary<ISkill, SkillEval> skillMap = new Dictionary<ISkill, SkillEval>();

			public SkillEval this[ISkill skill]
			{
				get
				{
					if (!skillMap.TryGetValue(skill, out SkillEval skillEval))
					{
						skillMap[skill] = skillEval = new SkillEval
						{
							Skill = skill
						};
					}
					return skillEval;
				}
			}
		}

		public class SkillEval
		{
			public ISkill Skill { get; set; }
			public bool HasSkill { get; set; } 			
			public bool SkillEvaluated { get; set; }
			public bool SkillResolved { get; set; }
			private Dictionary<IUnitModel, TargetEval> targetMap = new Dictionary<IUnitModel, TargetEval>();

			public TargetEval this[IUnitModel target]
			{
				get
				{
					if (!targetMap.TryGetValue(target, out TargetEval targetEval))
					{
						targetMap[target] = targetEval = new TargetEval
						{
							Target = target
						};
					}
					return targetEval;
				}
			}
		}

		public class TargetEval
		{
			public bool IsValidType { get; set; }
			public bool IsValidTarget { get; set; }			
			public bool IsValidConditions { get; set; }
			public IUnitModel Target { get; set; }
		}

		public class ConditionEval
		{
			public bool IsValid { get; set; }
			public IAITargetCondition Condition { get; set; }
		}
	}	
}
