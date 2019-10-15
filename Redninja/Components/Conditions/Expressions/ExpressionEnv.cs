using System;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Conditions.Expressions
{
	internal class ExpressionEnv : IExpressionEnv
	{
		private IBattleEntity self;
		private IBattleEntity target;
		private IBattleEntity source;
		private IBattleModel battleModel;
		private ICombatEvent battleEvent;

		public IBattleEntity Self => ReturnOrThrow(self, "SELF");
		public IBattleEntity Source => ReturnOrThrow(source, "SOURCE");
		public IBattleEntity Target => ReturnOrThrow(self, "TARGET");
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

		public static ExpressionEnv From(IBattleModel model, IBattleEntity self, IBattleEntity target)
		{
			ExpressionEnv env = new ExpressionEnv();
			env.self = self;
			env.battleModel = model;
			env.target = target;
			return env;
		}

		public static ExpressionEnv From(IBattleModel model, IBattleEntity self, ICombatEvent battleEvent)
		{
			ExpressionEnv env = new ExpressionEnv();
			env.source = battleEvent.Source;
			env.self = self;
			env.battleModel = model;
			env.target = battleEvent.Target;
			env.battleEvent = battleEvent;
			return env;
		}
	}
}
