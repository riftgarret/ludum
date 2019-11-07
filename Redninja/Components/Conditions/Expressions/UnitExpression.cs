using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Conditions.Expressions
{
	public class UnitExpression : IExpression
	{
		private const string STAT_PREFIX = "s_";
		private const string LIVE_PREFIX = "v_";

		public ExpressionResultType ResultType { private set; get; }

		private NextExpressionMeta<IBattleEntity> nextMeta;

		public UnitExpression(string raw)
		{
			if(raw == null) return;

			var split = raw.SplitOn('.');			

			nextMeta = parseNext(split);
		}

		private NextExpressionMeta<IBattleEntity> parseNext(ExpressionSplit split)
		{
			var nextMeta = new NextExpressionMeta<IBattleEntity>()
			{
				Name = split.parsed,
			};
			
			if(split.parsed.StartsWith(STAT_PREFIX, StringComparison.OrdinalIgnoreCase))
			{
				// stat parsing
				var statVal = split.parsed.Split('_')[1];
				if (!Enum.TryParse(statVal, true, out Stat stat))
					throw new InvalidOperationException($"Unable to parse stat prefix: {split.parsed}");

				nextMeta.Expression = new NumberExpression();
				nextMeta.Extractor = (env, x) => (float) x.Stats[stat];				
			} else if(split.parsed.StartsWith(LIVE_PREFIX, StringComparison.OrdinalIgnoreCase))
			{
				// livestat parsing
				var statVal = split.parsed.Split('_')[1];
				bool isPercent = false;
				if(statVal.EndsWith("%"))
				{
					statVal = statVal.Substring(0, statVal.Length - 1);
					isPercent = true;
				}

				if (!Enum.TryParse(statVal, true, out LiveStat stat))
					throw new InvalidOperationException($"Unable to parse stat prefix: {split.parsed}");

				nextMeta.Expression = new NumberExpression();
				nextMeta.Extractor = (env, x) => (isPercent? x.LiveStats[stat].Percent * 100 : x.LiveStats[stat].Current);
			} else
			{
				throw new InvalidOperationException($"Unknown unit property: {split.parsed}");
			}

			return nextMeta;
		}

			public object GetResult(IExpressionEnv env, object unit)
		{
			if (nextMeta == null) return unit;

			return nextMeta.Expression.GetResult(env, nextMeta.Extractor(env, (IBattleEntity)unit));
		}
	}
}
