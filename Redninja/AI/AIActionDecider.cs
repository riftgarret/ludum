﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.AI
{
	public class AIActionDecider : IActionDecider
	{
		private AIRuleSet ruleSet;

		public bool IsPlayer => false;

		public AIActionDecider(AIRuleSet ruleSet)
		{
			this.ruleSet = ruleSet;
		}

		public event Action<IBattleEntity, IBattleAction> ActionSelected;


		public void ProcessNextAction(IBattleEntity entity, IBattleEntityManager entityManager)
		{
			IBattleAction action = ruleSet.ResolveAction(entity, entityManager);
			ActionSelected?.Invoke(entity, action);
		}
	}
}
