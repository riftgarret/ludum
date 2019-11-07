using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Conditions.Expressions
{
	public class BattleModelExpression : IExpression
	{
		private enum BattleModelExpressionType
		{
			Ally,
			AllyNotSelf,
			Enemy,
			Unit,			
			GameTime
		}

		private NextExpressionMeta<IBattleModel> nextMeta;
		private BattleModelExpressionType expressionType;

		public ExpressionResultType ResultType => ExpressionResultType.Battle;

		public BattleModelExpression(string raw)
		{
			var split = raw.SplitOn('.');

			if (Enum.TryParse(split.parsed, true, out expressionType))
			{
				nextMeta = parseNext(expressionType, split);
			}
			else
			{
				throw new InvalidOperationException($"Unable to parse rootVar: {raw}");
			}
		}

		private NextExpressionMeta<IBattleModel> parseNext(BattleModelExpressionType rootType, ExpressionSplit split)
		{
			var nextMeta = new NextExpressionMeta<IBattleModel>()
			{
				Name = split.parsed,
			};

			switch (rootType)
			{
				case BattleModelExpressionType.Ally:
					nextMeta.Expression = new UnitExpression(split.nextRaw);
					nextMeta.Extractor = (env, x) => x.Entities.Where(u => u.Team == env.Self.Team); break;
					break;
				case BattleModelExpressionType.AllyNotSelf:
					nextMeta.Expression = new UnitExpression(split.nextRaw);
					nextMeta.Extractor = (env, x) => x.Entities.Where(u => u.Team == env.Self.Team && u != env.Self);
					break;
				case BattleModelExpressionType.Enemy:
					nextMeta.Expression = new UnitExpression(split.nextRaw);
					nextMeta.Extractor = (env, x) => x.Entities.Where(u => u.Team != env.Self.Team); ;
					break;
				case BattleModelExpressionType.Unit:
					nextMeta.Expression = new UnitExpression(split.nextRaw);
					nextMeta.Extractor = (env, x) => x.Entities;
					break;
				case BattleModelExpressionType.GameTime:
					nextMeta.Expression = new NumberExpression();
					nextMeta.Extractor = (env, x) => x.Time;
					break;
				default:
					throw new InvalidOperationException($"Unsupported type: {rootType}");
			}

			return nextMeta;
		}

		public object GetResult(IExpressionEnv env, object param)
		{
			if (nextMeta == null) throw new InvalidOperationException("Battle Expression cannot be null"); // doesnt work in this class..

			switch(expressionType)
			{
				case BattleModelExpressionType.Ally:
				case BattleModelExpressionType.AllyNotSelf:
				case BattleModelExpressionType.Enemy:
				case BattleModelExpressionType.Unit:
					IEnumerable<IBattleEntity> entities = (IEnumerable<IBattleEntity>) nextMeta.Extractor(env, (IBattleModel)param);
					return entities.Select(x => nextMeta.Expression.GetResult(env, x));					
			}

			return nextMeta.Expression.GetResult(env, nextMeta.Extractor(env, (IBattleModel)param));
		}
	}
}
