using System;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Conditions.Expressions
{
	public class ExpressionEnv : IExpressionEnv
	{
		private IBattleEntity self;
		private IBattleEntity target;		
		private IBattleModel battleModel;
		private ICombatEvent battleEvent;

		public IBattleEntity Self => ReturnOrThrow(self, "SELF");		
		public IBattleEntity Target => ReturnOrThrow(target, "TARGET");
		public IBattleModel BattleModel => ReturnOrThrow(battleModel, "BATTLE");
		public ICombatEvent BattleEvent => ReturnOrThrow(battleEvent, "EVENT");

		private ExpressionEnv()
		{
		}

		private T ReturnOrThrow<T>(T item, string propName) where T : class
		{
			if (item == null) throw new InvalidOperationException($"Missing environment property: {propName}");
			return item;
		}

		public class ExpressionEnvBuilder : BuilderBase<IExpressionEnv, ExpressionEnvBuilder>
		{
			private ExpressionEnv env;

			public ExpressionEnvBuilder() => Reset();

			public override ExpressionEnvBuilder Reset() => Self(x => env = new ExpressionEnv());

			public ExpressionEnvBuilder Self(IBattleEntity self) => Self(x => env.self = self);

			public ExpressionEnvBuilder Target(IBattleEntity target) => Self(x => env.target = target);

			public ExpressionEnvBuilder Context(IBattleContext context) => Self(x => env.battleModel = context.BattleModel);

			public ExpressionEnvBuilder Event(ICombatEvent e) => Self(x => env.battleEvent = e);
			
			public override IExpressionEnv Build()
			{
				return env;
			}
		}
	}
}
