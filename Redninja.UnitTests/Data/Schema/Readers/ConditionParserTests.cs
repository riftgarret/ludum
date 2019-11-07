using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Conditions;
using Redninja.Text;

namespace Redninja.Data.Schema.Readers.UnitTests
{
	[TestFixture]
	public class ConditionParserTests : TestBase
	{
		[TestCase("REQUIRE < 51", "< 51")]
		[TestCase("REQUIRE <= 1", "<= 1")]
		[TestCase("REQUIRE = 0", "= 0")]
		[TestCase("REQUIRE != 2", "!= 2")]
		[TestCase("REQUIRE > 1", "> 1")]
		[TestCase("REQUIRE >= 5", ">= 5")]
		[TestCase("require ALL", "ALL")]
		[TestCase("require Any", "Any")]
		public void AddRequireCapture(string input, string expected)
		{
			RegexPatternBuilder builder = RegexPatternBuilder.Begin();
			ConditionParser.AddRequireCapture(builder, "test");
			var pattern = builder.Build();

			var match = Regex.Match(input, pattern);
			Assert.That(match.Groups.Count, Is.EqualTo(2));
			Assert.That(match.Groups["test"].Success, Is.True);
			Assert.That(match.Groups["test"].Value, Is.EqualTo(expected));
		}

		[TestCase("SELF.EXP1.EXP2", "SELF", "EXP1", "EXP2")]
		[TestCase("Ally.Helper.HP%", "Ally", "Helper", "HP%")]
		[TestCase("Ally", "Ally")]
		public void AddExpCapture(string input, params string[] expectedArray)
		{
			RegexPatternBuilder builder = RegexPatternBuilder.Begin();
			ConditionParser.AddExpCapture(builder, "test");
			var pattern = builder.Build();

			var match = Regex.Match(input, pattern);
			Assert.That(match.Groups.Count, Is.EqualTo(2));
			Assert.That(match.Groups["test"].Success, Is.True);

			var result = Enumerable.Range(1, match.Groups["test"].Captures.Count)
								   .Select(i => match.Groups["test"].Captures[i - 1].Value);
			var expected = new List<string>(expectedArray);;

			Assert.That(result, Is.EquivalentTo(expected));
		}

		[TestCase("<")]
		[TestCase("<=")]
		[TestCase("=")]
		[TestCase("!=")]
		[TestCase(">=")]
		[TestCase(">")]
		public void AddOpCapture(string input)
		{
			RegexPatternBuilder builder = RegexPatternBuilder.Begin();
			ConditionParser.AddOpCapture(builder, "test");
			var pattern = builder.Build();

			var match = Regex.Match(input, pattern);
			Assert.That(match.Groups.Count, Is.EqualTo(2));
			Assert.That(match.Groups["test"].Success, Is.True);
		}

		[Test]
		public void SelfEqualsTarget()
		{
			ConditionParser parser = new ConditionParser();

			var success = parser.TryParseCondition("SELF = Target", out ICondition subject);
			Assert.That(success, Is.True);

			var unit = this.CreateEntity(10, 10);

			var result = subject.IsTargetConditionMet(unit, unit, Substitute.For<IBattleModel>());
			Assert.That(result, Is.True);
		}
	}
}
