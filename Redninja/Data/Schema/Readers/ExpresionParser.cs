using System;
using System.Text.RegularExpressions;
using Redninja.Components.Conditions;
using Redninja.Components.Conditions.Expressions;
using Redninja.Components.Utils;
using Redninja.Logging;

namespace Redninja.Data.Schema.Readers
{
	internal class ExpresionParser
	{

		private const string GROUP_UNIT = "unit";
		private const string GROUP_COMBAT_STAT = "stats";
		private const string GROUP_NUMBER_VALUE = "value";
		private const string GROUP_EXPGROUP = "group";

		private readonly string pattern;

		public ExpresionParser()
		{
			pattern = RegexPatternBuilder
				.Begin()
				.StartOptionSet()
				.AddCapture(GROUP_UNIT, GetEnumRegex(typeof(ConditionTargetType)))
				.NextOption()
				.AddCapture(GROUP_COMBAT_STAT, GetEnumRegex(typeof(CombatStats)))
				.NextOption()
				.AddCapture(GROUP_NUMBER_VALUE, @"\d+%?")
				.NextOption()
				.AddCapture(GROUP_EXPGROUP, GetEnumRegex(typeof(GroupOp)))
				.EndOptions()
				.Build();
		}


		/// <summary>
		/// Tries the parse expression. Attach this expression to the previous one as its used to verify
		/// and confirm creating specific expressions that should be aware of incoming types.
		/// </summary>
		/// <returns><c>true</c>, if parse expression was tryed, <c>false</c> otherwise.</returns>
		/// <param name="raw">Raw.</param>
		/// <param name="prevExpression">Init expression. This can be null.</param>
		/// <param name="expression">Expression.</param>
		internal bool TryParseExpression(string raw, IExpression prevExpression,  out IExpression expression)
		{
			bool result = _TryParseExpression(raw, prevExpression, out expression);
			if (result)
				return VerifyAndAttach(prevExpression, expression);
			return false;
		}

		private bool _TryParseExpression(string raw, IExpression prevExpression, out IExpression expression)
		{
			Match match = Regex.Match(raw, pattern);
			if (!match.Success) return FalseWithLog($"Unable to parse expression: {raw}", out expression);

			if (match.Groups[GROUP_UNIT].Success)
				return TryParseUnitExpression(match.Groups[GROUP_UNIT].Value, out expression);

			if (match.Groups[GROUP_NUMBER_VALUE].Success)
				return TryParseNumberExpression(match.Groups[GROUP_NUMBER_VALUE].Value, out expression);

			if (match.Groups[GROUP_EXPGROUP].Success)
				return TryParseGroupExpression(match.Groups[GROUP_EXPGROUP].Value, prevExpression, out expression);

			return FalseWithLog($"Missing exception match: {raw}", out expression);
		}

		/// <summary>
		/// Verify the and attach.
		/// </summary>
		/// <returns><c>true</c>, if and attach was verifyed, <c>false</c> otherwise.</returns>
		/// <param name="prevExpression">Previous expression.</param>
		/// <param name="expression">Expression.</param>
		private bool VerifyAndAttach(IExpression prevExpression, IExpression expression)
		{
			if (prevExpression == null)
				return true;

			if (!(prevExpression is IChainableExpression))
				return FalseWithLog($"exp is not chainable for assignment: {prevExpression}", out expression);
			IChainableExpression chainableExpression = (IChainableExpression)prevExpression;

			if (!(expression is IChainedExpression))
				return FalseWithLog($"exp is not chainned to be assignmented: {expression}", out expression);
			IChainedExpression chainedExpression = (IChainedExpression)expression;

			if (chainableExpression.ResultType != chainedExpression.Param)
				return FalseWithLog($"Invalid expression chain: {chainableExpression} -> {chainedExpression}", out expression);

			chainableExpression.ChainedExpression = chainedExpression;
			return true;
		}

		private bool TryParseNumberExpression(string raw, out IExpression expression)
		{
			int percIndex = raw.LastIndexOf('%');
			bool isPercent = percIndex > 0;
			raw = isPercent ? raw.Substring(0, percIndex) : raw;

			if (!int.TryParse(raw, out int number)) return FalseWithLog($"Unable to parse type from {raw}", out expression);

			expression = new NumberExpression(number, isPercent);
			return true;
		}

		private bool TryParseUnitExpression(string raw, out IExpression expression)
		{
			if (!Enum.TryParse(raw, out ConditionTargetType type)) return FalseWithLog($"Unable to parse type from {raw}", out expression);

			expression = new TargetUnitExpression(type);
			return true;
		}

		private bool TryParseGroupExpression(string raw, IExpression prevExpression, out IExpression expression)
		{
			if (!Enum.TryParse(raw, out GroupOp groupOp)) return FalseWithLog($"Unable to parse type from {raw}", out expression);

			expression = new GroupExpression(groupOp, prevExpression.ResultType);
			return VerifyAndAttach(prevExpression, expression);
		}

		private bool FalseWithLog(string log, out IExpression failedExpression)
		{
			RLog.E(this, log);
			failedExpression = null;
			return false;
		}


		private static string GetEnumRegex(Type enumType)
			=> string.Join("|", Enum.GetNames(enumType));

	}
}
