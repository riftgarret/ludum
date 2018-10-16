using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Actions;
using Redninja.Components.Operations;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Data.Schema.UnitTests
{
	[TestFixture]
	public class ParseHelperTests
	{
		[Test]
		public void GetTargetCondition()
		{
			TargetCondition none = ParseHelper.ParseTargetCondition("None");
			Assert.AreEqual(TargetConditions.None, none);
			Assert.IsTrue(none(null, null));
		}

		[Test]
		public void GetOperationProvider()
		{
			IUnitModel mEntity = Substitute.For<IUnitModel>();
			ITargetResolver mTarget = Substitute.For<ITargetResolver>();
			ISkill mSkill = Substitute.For<ISkill>();

			OperationProvider damage = ParseHelper.ParseOperationProvider("Damage");
			Assert.AreEqual(OperationProviders.Damage, damage);
			Assert.IsInstanceOf<DamageOperation>(damage(mEntity, mTarget, mSkill));
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

		public class PatternTestCases
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData("single", TargetPatternFactory.CreatePattern(new Coordinate(0, 0)));
					yield return new TestCaseData("(0,0)", TargetPatternFactory.CreatePattern(new Coordinate(0, 0)));
					yield return new TestCaseData("(1,0)", TargetPatternFactory.CreatePattern(new Coordinate(1, 0)));
					yield return new TestCaseData("(1,0), (1,1)", TargetPatternFactory.CreatePattern(new Coordinate(1, 0), new Coordinate(1,1)));
					yield return new TestCaseData("(1,0), (1,1) (2,1)", TargetPatternFactory.CreatePattern(new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(2, 1)));
					yield return new TestCaseData("(1 , 2)", TargetPatternFactory.CreatePattern(new Coordinate(1, 2)));


				}
			}
		}

		[Test, TestCaseSource(typeof(PatternTestCases), "TestCases")]
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
	}
}
