﻿using System;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Conditions.Expressions
{
	internal class ExpressionEnv : IExpressionEnv
	{
		private IUnitModel self;
		private IUnitModel target;
		private IUnitModel source;
		private IBattleModel battleModel;
		private IBattleEvent battleEvent;

		public IUnitModel Self => ReturnOrThrow(self, "SELF");
		public IUnitModel Source => ReturnOrThrow(source, "SOURCE");
		public IUnitModel Target => ReturnOrThrow(self, "TARGET");
		public IBattleModel BattleModel => ReturnOrThrow(battleModel, "BATTLE");
		public IBattleEvent BattleEvent => ReturnOrThrow(battleEvent, "EVENT");

		private ExpressionEnv()
		{
		}

		private T ReturnOrThrow<T>(T item, string propName) where T : class
		{
			if (item == null) throw new InvalidOperationException($"Missing environment property: {propName}");
			return item;
		}

		public static ExpressionEnv From(IBattleModel model, IUnitModel self, IUnitModel target)
		{
			ExpressionEnv env = new ExpressionEnv();
			env.self = self;
			env.battleModel = model;
			env.target = target;
			return env;
		}

		public static ExpressionEnv From(IBattleModel model, IUnitModel self, IBattleEvent battleEvent)
		{
			ExpressionEnv env = new ExpressionEnv();
			env.source = battleEvent.Entity;
			env.self = self;
			env.battleModel = model;
			if (battleEvent is ITargetedEvent)
				env.target = ((ITargetedEvent)battleEvent).Target;
			env.battleEvent = battleEvent;
			return env;
		}
	}
}
