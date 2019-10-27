using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Actions;
using Redninja.Components.Targeting;
using Redninja.Entities;
using static Redninja.Components.Decisions.AI.AIActionDecisionResult;

namespace Redninja.Components.Decisions.AI.UnitTests
{
	[TestFixture]
	public class AIExecutorTests
	{
		private IBattleEntity mSource;
		private IAIRuleTracker mHistory;		
		private IBattleContext mContext;
		private AIRuleSet behavior;
		private Tracker tracker;

		protected List<IBattleEntity> allEntities;

		private AIExecutor subject;
		private int ruleCounter;		

		[SetUp]
		public void Setup()
		{
			ruleCounter = 0;
			behavior = new AIRuleSet();
			mContext = Substitute.For<IBattleContext>();
			mSource = Substitute.For<IBattleEntity>();
			mHistory = Substitute.For<IAIRuleTracker>();
			allEntities = new List<IBattleEntity>() { mSource };
			mContext.BattleModel.Entities.Returns(allEntities);
			tracker = new Tracker();

			subject = Substitute.ForPartsOf<AIExecutor>(mContext, mSource, behavior, mHistory);
		}

		private IAIRule CreateMockRule(int weight = 1, string name = null)
		{
			if (name == null) name = $"generated rule {ruleCounter++}";

			IAIRule rule = Substitute.For<IAIRule>();

			rule.RuleName.Returns(name);
			rule.Weight.Returns(weight);
			behavior.Rules.Add(rule);

			return rule;
		}

		protected IBattleEntity AddEntity(int teamId)
		{
			var mEntity = Substitute.For<IBattleEntity>();
			mEntity.Team.Returns(teamId);
			allEntities.Add(mEntity);
			return mEntity;
		}

		protected IAITargetCondition AddMockCondition(TargetTeam target, bool isValid, IBattleEntity entityArg = null)
		{
			IAITargetCondition mockCondition = Substitute.For<IAITargetCondition>();
			mockCondition.IsValid(entityArg ?? Arg.Any<IBattleEntity>()).Returns(isValid);
			// TODO
			return mockCondition;
		}


		[TestCase(4, 3)]
		[TestCase(1, 0)]
		[TestCase(2, 0)]
		[TestCase(2, 1)]
		[TestCase(15, 3)]
		[TestCase(12, 11)]
		public void ResolveAction_FindsAction(int numberOfRules, int ruleIndexToUse)
		{
			IBattleAction actionOut = null;
			IBattleAction expected = Substitute.For<IBattleAction>();

			var rules = Enumerable.Range(1, numberOfRules).Select(x => CreateMockRule()).ToList();
			var expectedRule = rules[ruleIndexToUse];

			subject.When(x => x.GetValidRules(Arg.Any<Tracker>())).DoNotCallBase();
			subject.GetValidRules(tracker).ReturnsForAnyArgs(rules);

			subject.When(x => x.TryGetAction(tracker, Arg.Any<IAIRule>(), out actionOut)).DoNotCallBase();
			subject.TryGetAction(tracker, null, out actionOut).ReturnsForAnyArgs(x =>
			{
				bool correctRule = x[1] == expectedRule;
				x[2] = correctRule? expected : null;
				return correctRule;
			});

			var result = subject.ResolveAction();

			Assert.That(result.Result, Is.EqualTo(expected));
			mHistory.Received().AddEntry(expectedRule, expected);
		}

		private List<int> ParseDigitsToList(string raw)
			=> raw.ToCharArray().Select(c => int.Parse($"{c}")).ToList();


		[TestCase("", "24", "56")]
		[TestCase("0", "0", "0")]
		[TestCase("35", "0345", "53")]
		[TestCase("024", "0248", "01234")]
		public void GetValidRules(
			string expectedSubsetStr,
			string rulesReadyStr, 
			string rulesValidStr)
		{
			var originalRules = Enumerable.Range(1, 10).Select(x => CreateMockRule()).ToList();
			
			var validRules = ParseDigitsToList(rulesValidStr).Select(x => originalRules[x]);
			var rulesReady = ParseDigitsToList(rulesReadyStr).Select(x => originalRules[x]);
			var expectedRules = ParseDigitsToList(expectedSubsetStr).Select(x => originalRules[x]);

			subject.When(x => x.IsValidTriggerConditions(Arg.Any<Tracker>(), Arg.Any<IAIRule>())).DoNotCallBase();			
			subject.IsValidTriggerConditions(tracker, null).ReturnsForAnyArgs(x => validRules.Contains(x[1]));

			mHistory.IsRuleReady(null).ReturnsForAnyArgs(x => rulesReady.Contains(x[0]));			

			var result = subject.GetValidRules(tracker);

			Assert.That(result, Is.EquivalentTo(expectedRules));
		}

		[TestCase("", "24", "56", "4", "6")]
		[TestCase("0", "0", "0")]
		[TestCase("35", "0345", "53")]
		[TestCase("02", "0248", "01234", "02567")]
		public void GetValidTargets(
			string expectedSubsetStr,			
			string validTargetsStr, 						
			params string[] filteredContionSetStr)
		{
			var originalEntities = Enumerable.Range(1, 10).Select(x => Substitute.For<IBattleEntity>()).ToList();			
			
			var isValid = ParseDigitsToList(validTargetsStr).Select(x => originalEntities[x]);			
			var expectedEntities = ParseDigitsToList(expectedSubsetStr).Select(x => originalEntities[x]);
			var conditions = new List<string>(filteredContionSetStr)
				.Select(filter =>
				{
					var allowedEntities = ParseDigitsToList(filter).Select(x => originalEntities[x]);
					var cond = Substitute.For<IAITargetCondition>();
					cond.IsValid(null).ReturnsForAnyArgs(x => allowedEntities.Contains(x[0]));
					return cond;
				}).ToList();
		
			var mRule = Substitute.For<IAISkillRule>();
			mRule.FilterConditions.Returns(conditions);
			var mTargetingRule = Substitute.For<ITargetingRule>();
			mTargetingRule.IsValidTarget(null, null).ReturnsForAnyArgs(x => isValid.Contains(x[0]));

			subject.When(x => x.FilterByType(Arg.Any<TargetTeam>())).DoNotCallBase();
			subject.FilterByType(Arg.Any<TargetTeam>()).Returns(x => originalEntities);

			var result = subject.GetValidSkillTargets(new SkillEval(), mRule, mTargetingRule);

			Assert.That(result, Is.EquivalentTo(expectedEntities));
		}
	}
}
