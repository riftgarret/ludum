using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Conditions.Expressions
{
	public class EventExpression : IExpression
	{
		public ExpressionResultType ResultType { get; } = ExpressionResultType.Environment;

		public IExpression Next { get; } 

		private NextExpressionMeta<ICombatEvent> nextMeta;		

		private enum EventExpressionType
		{
			Source,
		}

		public EventExpression(string raw)
		{
			var split = raw.SplitOn('.');

			if(!Enum.TryParse(split.parsed, true, out EventExpressionType rootType))
				throw new InvalidOperationException($"Unable to parse rootVar: {raw}");

			nextMeta = parseNext(rootType, split);
		}

		private NextExpressionMeta<ICombatEvent> parseNext(EventExpressionType rootType, ExpressionSplit split)
		{
			var nextMeta = new NextExpressionMeta<ICombatEvent>()
			{
				Name = split.parsed,				
			};

			switch (rootType)
			{
				case EventExpressionType.Source:
					nextMeta.Expression = new UnitExpression(split.nextRaw);
					nextMeta.Extractor = (env, x) => x.Source;
					break;
					throw new InvalidOperationException($"Unsupported type: {rootType}");
			}

			return nextMeta;
		}

		public object GetResult(IExpressionEnv env, object e)
		{
			if(nextMeta == null) throw new InvalidOperationException("Next Env Expression cannot be null"); // doesnt work in this class..

			return nextMeta.Expression.GetResult(env, nextMeta.Extractor(env, (ICombatEvent) e));
		}
	}
}
