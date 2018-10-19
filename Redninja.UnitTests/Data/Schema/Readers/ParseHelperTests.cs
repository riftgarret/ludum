﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Actions;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Operations;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema.Readers.UnitTests
{
	[TestFixture]
	public class ParseHelperTests
	{
		[Test]
		public void ParseTargetCondition()
		{
			TargetCondition result = ParseHelper.ParseTargetCondition("None");
			Assert.AreEqual(TargetConditions.None, result);
			Assert.IsTrue(result(null, null));
		}

		[Test]
		public void ParseOperationProvider()
		{
			IUnitModel mEntity = Substitute.For<IUnitModel>();
			ITargetResolver mTarget = Substitute.For<ITargetResolver>();
			ISkill mSkill = Substitute.For<ISkill>();

			OperationProvider result = ParseHelper.ParseOperationProvider("Damage");
			Assert.AreEqual(OperationProviders.Damage, result);
			Assert.IsInstanceOf<DamageOperation>(result(mEntity, mTarget, mSkill));
		}

		public static IEnumerable ParseAITargetConditionTestCases
		{
			get
			{
				yield return new TestCaseData("AlwaysTrue", AIConditionFactory.AlwaysTrue);
				yield return new TestCaseData("HP > 50", AIConditionFactory.CreateCombatStatCondition(
					50, CombatStats.HP, AIValueConditionOperator.GT, AIConditionType.CombatStatCurrent));
				yield return new TestCaseData("Resource = 100%", AIConditionFactory.CreateCombatStatCondition(
					100, CombatStats.Resource, AIValueConditionOperator.EQ, AIConditionType.CombatStatPercent));
				yield return new TestCaseData("DEF <= 20", AIConditionFactory.CreateCombatStatCondition(
					20, CombatStats.DEF, AIValueConditionOperator.LTE, AIConditionType.CombatStatCurrent));
				yield return new TestCaseData("HP >= 9%", AIConditionFactory.CreateCombatStatCondition(
					9, CombatStats.HP, AIValueConditionOperator.GTE, AIConditionType.CombatStatPercent));
				yield return new TestCaseData("ATK < 99%", AIConditionFactory.CreateCombatStatCondition(
					99, CombatStats.ATK, AIValueConditionOperator.LT, AIConditionType.CombatStatPercent));
			}
		}

		[Test, TestCaseSource("ParseAITargetConditionTestCases")]
		public void ParseAITargetCondition(string text, IAITargetCondition expected)
		{
			var result = ParseHelper.ParseAITargetCondition(text);
			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void ParseAITargetPriority()
		{
			var result = ParseHelper.ParseAITargetPriority("Any");
			Assert.That(result, Is.EqualTo(AITargetPriorityFactory.Any));
		}

		[TestCase("[3,4,1]", 3, 4, 1)]
		[TestCase("[0.1, 2.5, 10.0]", 0.1f, 2.5f, 10f)]
		[TestCase("[0, 0,0]", 0, 0, 0)]
		public void ParseActionTime_Success(string json, params float[] expected)
		{
			ActionTime expectedTime = new ActionTime(expected[0], expected[1], expected[2]);
			var raw = JsonConvert.DeserializeObject<List<float>>(json);

			var result = ParseHelper.ParseActionTime(raw);
			Assert.That(result, Is.EqualTo(expectedTime));
		}

		[TestCase("[3]")]
		[TestCase("[3,1,1,0]")]
		[TestCase("[]")]
		public void ParseActionTime_InvalidFormat_FormatException(string json)
		{
			var raw = JsonConvert.DeserializeObject<List<float>>(json);

			Assert.Throws<FormatException>(() => ParseHelper.ParseActionTime(raw));
		}

		[Test]
		public void ParseActionTime_NullParam_InvalidOperationException()
		{
			Assert.Throws<InvalidOperationException>(() => ParseHelper.ParseActionTime(null));
		}


		public static IEnumerable ParsePatternTestCases
		{
			get
			{
				yield return new TestCaseData("single", TargetPatternFactory.CreatePattern(new Coordinate(0, 0)));
				yield return new TestCaseData("(0,0)", TargetPatternFactory.CreatePattern(new Coordinate(0, 0)));
				yield return new TestCaseData("(1,0)", TargetPatternFactory.CreatePattern(new Coordinate(1, 0)));
				yield return new TestCaseData("(1,0), (1,1)", TargetPatternFactory.CreatePattern(new Coordinate(1, 0), new Coordinate(1, 1)));
				yield return new TestCaseData("(1,0), (1,1) (2,1)", TargetPatternFactory.CreatePattern(new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(2, 1)));
				yield return new TestCaseData("(1 , 2)", TargetPatternFactory.CreatePattern(new Coordinate(1, 2)));
			}
		}

		[Test, TestCaseSource(typeof(ParseHelperTests), "ParsePatternTestCases")]
		public void ParsePattern_MatchesPattern(string pattern, ITargetPattern resultPattern)
		{
			var result = ParseHelper.ParsePattern(pattern);

			Assert.That(result, Is.EqualTo(resultPattern));
		}

		[TestCase("(0,)")]
		[TestCase("()")]
		[TestCase("")]
		[TestCase("(0,)32")]
		[TestCase("(,1)")]
		[TestCase("1(0,)")]
		public void ParsePattern_InvalidFormat_FormatException(string pattern)
		{
			Assert.Throws<FormatException>(() => ParseHelper.ParsePattern(pattern));
		}

		[Test]
		public void ParsePattern_NullPattern_InvalidOperationException()
		{
			Assert.Throws<InvalidOperationException>(() => ParseHelper.ParsePattern(null));
		}

		[TestCase("1,2", 1, 2)]
		[TestCase("0,0", 0, 0)]
		[TestCase("-1, 4", -1, 4)]
		public void ParseCoordinate_Success(string text, params int[] expectedInts)
		{
			Coordinate expected = new Coordinate(expectedInts[0], expectedInts[1]);
			Coordinate result = ParseHelper.ParseCoordinate(text);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void ParseCoordinate_FailsNull()
		{
			Assert.Throws<InvalidOperationException>(() => ParseHelper.ParseCoordinate(null));
		}

		[TestCase("3 1,1")]
		[TestCase("3,2,1")]
		[TestCase("")]
		[TestCase("123")]
		public void ParseCoordinate_FailsInvalidFormat(string text)
		{
			Assert.Throws<FormatException>(() => ParseHelper.ParseCoordinate(text));
		}
	}
}
