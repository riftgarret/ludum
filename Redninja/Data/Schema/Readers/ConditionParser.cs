using System;
using System.Text.RegularExpressions;
using Redninja.Components.Utils;

namespace Redninja.Data.Schema.Readers
{
	public class ConditionParser
	{
		const string OP_CAPTURE = @"(?<op>[\>\<=\!])";
		const string LEFT_EXP_CAPTURE = @"(?<left_target>\D+)\.(?<left_prop>\D+)";
		const string RIGHT_EXP_CAPTURE = @"(?<val>\d+)(?<perc>%?)(?<target>\D+)\.(?<prop>\D+)";

		private const string OP_EQUALITY_ALL = @"\>|\<|=|\>=|\<=";
		private const string OP_EQUALITY_ONLY = @"=|\!=";
		private const string OP_GROUP = "op";

		private const string CLASS_VALUES = @"\D+";
		private const string COMBAT_STAT_VALUES = @"\d+%?";

		private const string GROUP_LEFT_TARGET = "left_target";
		private const string GROUP_LEFT_STAT = "left_stat";
		private const string GROUP_RIGHT_STAT = "right_stat";
		private const string GROUP_VALUE = "value";
		private const string TARGET_VALUES = "SELF|TARGET";




		private string combatStatPattern;
		private string classPattern;
		private string pattern;

		public ConditionParser()
		{
			combatStatPattern = RegexPatternBuilder.Begin()
				.AddCapture(GROUP_LEFT_TARGET, TARGET_VALUES)
				.AddNonCapture(@"\.")
				.AddCapture(GROUP_LEFT_STAT, @"\w+")
				.AddWhiteSpaceOptional()
				.AddCapture(OP_GROUP, OP_EQUALITY_ALL)
				.AddWhiteSpaceOptional()
				.StartOptionSet()
					.AddCapture(GROUP_RIGHT_STAT, TARGET_VALUES)
					.AddNonCapture(@"\.")
					.AddCapture(GROUP_RIGHT_STAT, @"\w+")
				.NextOption()
					.AddCapture(GROUP_VALUE, @"\d+%?")
				.EndOptions()
				.Build();
		}

		public object ParseCondition(string raw)
		{
			return null;
		}
	}
}
