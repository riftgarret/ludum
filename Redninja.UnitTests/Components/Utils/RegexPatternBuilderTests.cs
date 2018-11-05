using System;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Redninja.Components.Utils.UnitTests
{
	/// <summary>
	/// Note about 'Count' checking on groups. There is a group for each
	/// capture group name regardless, and there is a group for the full
	/// match. So even if you expect only 1 match, at minimum there would be
	/// 2 for matching the full input.
	/// </summary>
	[TestFixture]
	public class RegexPatternBuilderTests
	{

		[TestCase(@"\d+", "32", "32")]
		[TestCase(@"\w+", "common", "common")]
		[TestCase(@"[^\s]+", "hello", "hello")]
		public void BuildCaptureOne(string regex, string input, string expected)
		{
			string groupId = "hi";
			string pattern = RegexPatternBuilder.Begin()
				.AddCapture(groupId, regex)				
				.Build();

			var match = Regex.Match(input, pattern);
			var result = match.Groups[groupId].Value;
			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void BuildCapture_TwoAdjascent()
		{		
			string pattern = RegexPatternBuilder
				.Begin()
				.AddCapture("first", "SELF|TARGET")
				.AddWhiteSpaceOptional()
				.AddCapture("second", @"\d+")
				.Build();				

			var match = Regex.Match("TARGET 321", pattern);

			var first = match.Groups["first"].Value;
			var second = match.Groups["second"].Value;

			Assert.That(match.Groups.Count, Is.EqualTo(3));
			Assert.That(first, Is.EqualTo("TARGET"));
			Assert.That(second, Is.EqualTo("321"));
		}

		[Test]
		public void BuildOptional_SecondOption()
		{
			string pattern = RegexPatternBuilder
				.Begin()
				.StartOptionSet()
				.AddCapture("opt1", "SELF")
				.NextOption()
				.AddNonCapture(@"ITS\s")
				.AddCapture("opt2", "ME MARIO")
				.EndOptions()
				.AddWhiteSpaceOptional()
				.AddCapture("cap1", "Hello World")
				.Build();

			var match = Regex.Match("ITS ME MARIO    Hello World", pattern);
			var first = match.Groups["opt2"].Value;
			var second = match.Groups["cap1"].Value;

			Assert.That(match.Groups.Count, Is.EqualTo(4));
			Assert.That(first, Is.EqualTo("ME MARIO"));
			Assert.That(second, Is.EqualTo("Hello World"));
		}

		[Test]
		public void BuildOptional_FirstOption()
		{
			string pattern = RegexPatternBuilder
				.Begin()
				.StartOptionSet()
				.AddCapture("opt1", "SELF")
				.NextOption()
				.AddNonCapture(@"ITS\s")
				.AddCapture("opt2", "ME MARIO")
				.EndOptions()
				.AddWhiteSpaceOptional()
				.AddCapture("cap1", "Hello World")
				.Build();

			var match = Regex.Match("SELF  Hello World", pattern);
			var first = match.Groups["opt1"].Value;

			Assert.That(match.Groups.Count, Is.EqualTo(4));
			Assert.That(first, Is.EqualTo("SELF"));
		}

		[Test]
		public void BuildRepatingPattern()
		{
			var pattern = RegexPatternBuilder
				.Begin()
				.AddCapture("word", @"\w+")
				.StartOptionSet()
				.AddNonCapture(@"\.")
				.AddCapture("word", @"\w+")
				.EndOptions("*")
				.Build();

			var match = Regex.Match("The.Brown.Fox", pattern);

			Assert.That(match.Groups["word"].Captures.Count, Is.EqualTo(3));
			Assert.That(match.Groups.Count, Is.EqualTo(2));

			Assert.That(match.Groups["word"].Captures[0].Value, Is.EqualTo("The"));
			Assert.That(match.Groups["word"].Captures[1].Value, Is.EqualTo("Brown"));
			Assert.That(match.Groups["word"].Captures[2].Value, Is.EqualTo("Fox"));
		}
	}
}
