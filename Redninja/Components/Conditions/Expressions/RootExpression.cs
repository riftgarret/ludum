using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Conditions.Expressions
{
	public class RootExpression : IExpression
	{
		public ExpressionResultType ResultType { get; } = ExpressionResultType.Environment;
		
		private NextExpressionMeta<IExpressionEnv> nextMeta;
		private AggregatorType aggregator = AggregatorType.None;

		private enum RootExpressionType
		{
			BM,
			Target,
			Self,
			Event,
		}

		private enum AggregatorType
		{
			None,
			Highest,
			Lowest,
			Avg
		}

		public RootExpression(string raw)
		{
			// there are 3 routes the env can go:
			// 1. its a number 
			// 2. it has an aggregator, but requires 3
			// 3. it connects to an environment variable
			var split = raw.SplitOn('.');			

			if (float.TryParse(split.parsed, out float val))
			{
				nextMeta = new NextExpressionMeta<IExpressionEnv>()
				{
					Name = "Number",
					Expression = new NumberExpression(),
					Extractor = (x, env) => val
				};
				return;
			}

			if(Enum.TryParse(split.parsed, true, out aggregator))
			{
				// move the split to the next expression
				split = split.nextRaw.SplitOn('.');
			}

			if (Enum.TryParse(split.parsed, true, out RootExpressionType rootType))
			{									
				nextMeta = parseNext(rootType, split);
			} else
			{
				throw new InvalidOperationException($"Unable to parse rootVar: {split.parsed} {raw}");
			}
		}

		private NextExpressionMeta<IExpressionEnv> parseNext(RootExpressionType rootType, ExpressionSplit split)
		{
			var nextMeta = new NextExpressionMeta<IExpressionEnv>()
			{
				Name = split.parsed,				
			};

			switch (rootType)
			{
				case RootExpressionType.BM:
					nextMeta.Expression = new BattleModelExpression(split.nextRaw);
					nextMeta.Extractor = (env, x) => x.BattleModel;
					break;
				case RootExpressionType.Target:
					nextMeta.Expression = new UnitExpression(split.nextRaw);
					nextMeta.Extractor = (env, x) => x.Target;
					break;
				case RootExpressionType.Self:
					nextMeta.Expression = new UnitExpression(split.nextRaw);
					nextMeta.Extractor = (env, x) => x.Self;
					break;
				case RootExpressionType.Event:
					nextMeta.Expression = new EventExpression(split.nextRaw);
					nextMeta.Extractor = (env, x) => x.BattleEvent;
					break;
				default:
					throw new InvalidOperationException($"Unsupported type: {rootType}");
			}

			return nextMeta;
		}

		public object GetResult(IExpressionEnv env, object ignored)
		{
			if(nextMeta == null) throw new InvalidOperationException("Next Env Expression cannot be null"); // doesnt work in this class..

			object result = nextMeta.Expression.GetResult(env, nextMeta.Extractor(env, env));
			switch (aggregator)
			{				
				case AggregatorType.Highest:
					return toValues(result).Max();
				case AggregatorType.Lowest:
					return toValues(result).Min();
				case AggregatorType.Avg:
					return toValues(result).Average();
			}

			return result;
		}

		private IEnumerable<float> toValues(object values) => ((IEnumerable<object>)values).ToList().Cast<float>();
	}
}
