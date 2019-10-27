using System;
using System.Linq;
using System.Text.RegularExpressions;
using Redninja.Components.Conditions;
using Redninja.Components.Conditions.Expressions;
using Redninja.Components.Decisions.AI;
using Redninja.Logging;
using Redninja.Text;

namespace Redninja.Data.Schema.Readers
{
	internal class ExpressionParser
	{

		private const string GROUP_UNIT = "unit";
		private const string GROUP_STAT = "stats";
		private const string GROUP_NUMBER_VALUE = "value";
		private const string GROUP_EXPGROUP = "group";

		private readonly string initPattern;
		private readonly string chainPattern;

		public ExpressionParser()
		{
			initPattern = RegexPatternBuilder
				.Begin()
				.StartOptionSet()				
				.AddCapture(GROUP_UNIT, GetEnumRegexShort(typeof(ConditionTargetType)))
				.NextOption()
				.AddCapture(GROUP_NUMBER_VALUE, @"\d+%?")
				.EndOptions()
				.Build();

			chainPattern = RegexPatternBuilder
				.Begin()
				.StartOptionSet()
				// TODO: Stat.value, should find Stat, not the full word
				.AddCapture(GROUP_STAT, $"(?:{GetEnumRegexFull(typeof(LiveStat), typeof(CalculatedStat), typeof(Stat))})%?")
				.NextOption()
				.AddCapture(GROUP_EXPGROUP, GetEnumRegexShort(typeof(GroupOp)))
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
			if (prevExpression == null)
				return TryParseInitialExpression(raw, out expression);
			else
			{
				bool result = TryParseChainExpression(raw, prevExpression, out expression);
				if (result)
					return VerifyAndAttach(prevExpression, expression);
				return false;
			}
		}

		private bool TryParseChainExpression(string raw, IExpression prevExpression, out IExpression expression)
		{
			Match match = Regex.Match(raw, chainPattern);
			if (!match.Success) return FalseWithLog($"Unable to parse expression: {raw}", out expression);

			if (match.Groups[GROUP_STAT].Success)
				return TryParseCombatStatExpression(match.Groups[GROUP_STAT].Value, out expression);
				
			if (match.Groups[GROUP_EXPGROUP].Success)
				return TryParseGroupExpression(match.Groups[GROUP_EXPGROUP].Value, prevExpression, out expression);

			return FalseWithLog($"Missing exception match: {raw}", out expression);
		}

		private bool TryParseInitialExpression(string raw, out IExpression expression) 
		{
			Match match = Regex.Match(raw, initPattern);
			if (!match.Success) return FalseWithLog($"Unable to parse expression: {raw}", out expression);

			if (match.Groups[GROUP_UNIT].Success)
				return TryParseUnitExpression(match.Groups[GROUP_UNIT].Value, out expression);

			if (match.Groups[GROUP_NUMBER_VALUE].Success)
				return TryParseNumberExpression(match.Groups[GROUP_NUMBER_VALUE].Value, out expression);
				
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

			if (!(expression is IParamExpression))
				return FalseWithLog($"exp is not chainned to be assignmented: {expression}", out expression);
			IParamExpression paramExpression = (IParamExpression)expression;

			if (prevExpression.ResultType != paramExpression.Param)
				return FalseWithLog($"Invalid expression chain: {prevExpression} -> {paramExpression}", out expression);

			prevExpression.Next = paramExpression;
			return true;
		}

		private bool TryParseCombatStatExpression(string raw, out IExpression expression)
		{
			int percIndex = raw.LastIndexOf('%');
			bool isPercent = percIndex > 0;
			raw = isPercent ? raw.Substring(0, percIndex) : raw;

			Enum someStat = ParseHelper.ParseStatRaw(raw);

			IStatEvaluator statEvaluator = ParseHelper.CreateStatEval(someStat, isPercent);			
			expression = new StatExpression(statEvaluator);
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
			if (!Enum.TryParse(raw, true, out ConditionTargetType type)) return FalseWithLog($"Unable to parse type from {raw}", out expression);

			expression = new TargetUnitExpression(type);
			return true;
		}

		private bool TryParseGroupExpression(string raw, IExpression prevExpression, out IExpression expression)
		{
			if (!Enum.TryParse(raw, true, out GroupOp groupOp)) return FalseWithLog($"Unable to parse type from {raw}", out expression);

			IExpressionResultDef def = ResultDefFactory.From(prevExpression.ResultType);
			if (def.NativeType != typeof(int))
				return FalseWithLog("Invalid chained type, this currently only supports native definitions", out expression);
			expression = new GroupExpression(groupOp, prevExpression.ResultType);
			return true;
		}

		private bool FalseWithLog(string log, out IExpression failedExpression)
		{
			RLog.E(this, log);
			failedExpression = null;
			return false;
		}


		/// <summary>
		/// Creates an option of enums that use a full "Stats.HP", instead of "HP"
		/// </summary>
		/// <param name="enumTypes"></param>
		/// <returns></returns>
		private static string GetEnumRegexFull(params Type[] enumTypes)
			=> string.Join("|", enumTypes.Select(enumType => enumType.Name + "\\.\\w+"));

		/// <summary>
		/// Creates an option of enums that just use "HP" from Stats.HP
		/// </summary>
		/// <param name="enumTypes"></param>
		/// <returns></returns>
		private static string GetEnumRegexShort(params Type[] enumTypes)
			=> string.Join("|", enumTypes.SelectMany(enumType => Enum.GetNames(enumType)));

	}
}
