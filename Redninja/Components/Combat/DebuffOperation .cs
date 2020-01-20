using System;
using Davfalcon;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Components.Buffs;

namespace Redninja.Components.Combat
{
	internal class DebuffOperationDefinition : IBattleOperationDefinition
	{		
		public string BuffId { get; set; }		
		public IStats Stats { get; set; }		
		public float ExecutionStart { get; set; }

		public IBattleOperation CreateOperation(ISkill skill, IBattleEntity source, ITargetResolver target)
			=> new DebuffOperation(source, target, this, skill);
	}

	internal class DebuffOperation : IBattleOperation
	{
		private readonly IBattleEntity unit;
		private readonly ITargetResolver target;
		private readonly DebuffOperationDefinition def;
		private readonly ISkill skill;

		internal DebuffOperation(IBattleEntity unit, ITargetResolver target, DebuffOperationDefinition def, ISkill skill)
		{
			this.unit = unit ?? throw new ArgumentNullException(nameof(unit));
			this.target = target ?? throw new ArgumentNullException(nameof(target));
			this.def = def;
			this.skill = skill;
		}

		public float ExecutionStart => def.ExecutionStart;

		public void Execute(IBattleContext context)
		{
			foreach (IBattleEntity t in target.GetValidTargets(unit, context.BattleModel))
			{
				IBuff buff = context.DataManager.CreateInstance<IBuff>(def.BuffId);

				buff.InitializeBattleState(context, unit, t);
				// TODO adjust length of buff from skill or use buff definition?

				t.Buffs.Add(buff);

				// TODO notify changes
			}			
		}
	}
}
