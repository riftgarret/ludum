using System.Text.RegularExpressions;
using Redninja.Components.Conditions;
using Redninja.Components.Conditions.Operators;
using Redninja.Logging;
using Redninja.Text;

namespace Redninja.Data.Schema.Readers
{
	internal class ConditionParser
	{
		private const string REGEX_OP = @"\>|\<|=|!=|\>=|\<=";
		private const string REGEX_REQUIRE = @"any|all|(?:" + REGEX_OP + @")\s\d+";
		private const string REGEX_EXP = @"[\w%]+";

		private const string GROUP_REQUIRE = "require";
		private const string GROUP_OP = "op";

		private const string GROUP_LEFT_EXPRESSION = "left-exp";
		private const string GROUP_RIGHT_EXPRESSION = "right-exp";

		private readonly string pattern;

		private readonly ExpressionParser expresionParser;
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

			expresionParser = new ExpressionParser();
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


			if (!TryBuildExpressionChain(match.Groups[GROUP_LEFT_EXPRESSION], out IEnvExpression left))
				return FalseWithLog($"Unable to build left expression tree: {raw}");

			if (!conditionOpParser.TryParseOp(match.Groups[GROUP_OP].Value, out IConditionalOperator op))
				return FalseWithLog($"Unable to build operator: {raw}");

			if (!TryBuildExpressionChain(match.Groups[GROUP_RIGHT_EXPRESSION], out IEnvExpression right))
				return FalseWithLog($"Unable to build right expression tree: {raw}");

			IOperatorCountRequirement opRequirement = AnyOpRequirement.Instance;
			if(match.Groups[GROUP_REQUIRE].Success)
			{
				if (!requirementParser.TryParseRequirement(match.Groups[GROUP_REQUIRE].Value, out opRequirement))
					return FalseWithLog($"Unable to build requirement {raw}");
			}

			condition = new Condition(left, right, op, opRequirement);

			return true;
		}

		private bool TryBuildExpressionChain(Group regexGroup, out IEnvExpression initialExpression) 
		{
			initialExpression = null;

			if (!expresionParser.TryParseExpression(regexGroup.Captures[0].Value, null, out IExpression expression))
				return false;

			if (!(expression is IEnvExpression))
				return FalseWithLog($"Initial expression is not a InitialExpressionType {regexGroup.Captures[0].Value}");

			initialExpression = (IEnvExpression)expression;
			IExpression curChain = expression;

			for (int i = 1; i < regexGroup.Captures.Count; i++)
			{
				if (!expresionParser.TryParseExpression(regexGroup.Captures[i].Value, curChain, out IExpression chained))
					return false;
				curChain = chained;
			}

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
				.EndOptions();
		}

		internal static RegexPatternBuilder AddExpCapture(RegexPatternBuilder builder, string captureGroup)
		{
			return builder
				.AddCapture(captureGroup, REGEX_EXP)
				.StartOptionSet()
				.AddNonCapture(@"\.")
				.AddCapture(captureGroup, REGEX_EXP)
				.EndOptions("*");
		}

		internal static RegexPatternBuilder AddOpCapture(RegexPatternBuilder builder, string captureGroup)
		{
			return builder.AddCapture(captureGroup, REGEX_OP);
		}
	}
}
