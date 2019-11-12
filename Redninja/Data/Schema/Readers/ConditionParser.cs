using System.Text.RegularExpressions;
using Redninja.Components.Conditions;
using Redninja.Components.Conditions.Expressions;
using Redninja.Components.Conditions.Operators;
using Redninja.Logging;
using Redninja.Text;

namespace Redninja.Data.Schema.Readers
{
	internal class ConditionParser
	{
		private const string REGEX_OP = @"\>|\<|=|!=|\>=|\<=";
		private const string REGEX_REQUIRE = @"any|all|(?:" + REGEX_OP + @")\s\d+";
		private const string REGEX_EXP = @"[\w%\.]+";

		private const string GROUP_REQUIRE = "require";
		private const string GROUP_OP = "op";

		private const string GROUP_LEFT_EXPRESSION = "leftexp";
		private const string GROUP_RIGHT_EXPRESSION = "rightexp";

		private readonly string pattern;
		
		private readonly ConditionOpParser conditionOpParser;
		private readonly RequirementParser requirementParser;

		public ConditionParser()
		{
			RegexPatternBuilder builder = RegexPatternBuilder.Begin();
			AddRequireCapture(builder, GROUP_REQUIRE);
			builder.AddWhiteSpaceOptional();
			AddExpCapture(builder, GROUP_LEFT_EXPRESSION);
			builder.AddWhiteSpaceOptional();
			AddOpCapture(builder, GROUP_OP);
			builder.AddWhiteSpaceOptional();
			AddExpCapture(builder, GROUP_RIGHT_EXPRESSION);

			pattern = builder.Build();
			
			conditionOpParser = new ConditionOpParser();
			requirementParser = new RequirementParser();
		}

		public bool TryParseCondition(string raw, out ICondition condition)
		{
			condition = null;

			Match match = Regex.Match(raw, pattern);

			if (!match.Success) return FalseWithLog($"No match found: {raw}");
			if (!match.Groups[GROUP_LEFT_EXPRESSION].Success) return FalseWithLog($"Missing left expression: {raw}");
			if (!match.Groups[GROUP_RIGHT_EXPRESSION].Success) return FalseWithLog($"Missing right expression: {raw}");
			if (!match.Groups[GROUP_OP].Success) return FalseWithLog($"Missing OP expression: {raw}");

			IExpression left = new RootExpression(match.Groups[GROUP_LEFT_EXPRESSION].Value);
			IExpression right = new RootExpression(match.Groups[GROUP_RIGHT_EXPRESSION].Value);

			if (!conditionOpParser.TryParseOp(match.Groups[GROUP_OP].Value, out IConditionalOperator op))
				return FalseWithLog($"Unable to build operator: {raw}");

			IOperatorCountRequirement opRequirement = AnyOpRequirement.Instance;
			if(match.Groups[GROUP_REQUIRE].Success)
			{
				if (!requirementParser.TryParseRequirement(match.Groups[GROUP_REQUIRE].Value, out opRequirement))
					return FalseWithLog($"Unable to build requirement {raw}");
			}

			condition = new Condition(left, right, op, opRequirement);
			((Condition)condition).Raw = raw;
			return true;
		}
		
		private bool FalseWithLog(string log)
		{
			RLog.E(this, log);
			return false;
		}

		internal static RegexPatternBuilder AddRequireCapture(RegexPatternBuilder builder, string captureGroup)
		{
			return builder
				.StartOptionSet()
				.AddNonCapture(@"require\s")
				.AddCapture(captureGroup, REGEX_REQUIRE)
				.EndOptions("?");
		}

		internal static RegexPatternBuilder AddExpCapture(RegexPatternBuilder builder, string captureGroup)
		{
			return builder
				.AddCapture(captureGroup, REGEX_EXP);				
		}

		internal static RegexPatternBuilder AddOpCapture(RegexPatternBuilder builder, string captureGroup)
		{
			return builder.AddCapture(captureGroup, REGEX_OP);
		}
	}
}
