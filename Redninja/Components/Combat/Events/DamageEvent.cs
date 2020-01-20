using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Combat.Events
{
	public enum DamageSourceType
	{
		Skill,
		Tick,
		Revenge
	}

	public class DamageEvent : ICombatEvent
	{		
		public IBattleEntity Source { get; }
		public IBattleEntity Target { get; }

		public DamageSourceType DamageSourceType => OperationResult.SourceType;
		
		public OperationResult OperationResult { get; }

		public DamageType DamageType => OperationResult.DamageType;
		
		internal DamageEvent(IBattleEntity source, IBattleEntity target, OperationResult operationResult)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Target = target ?? throw new ArgumentNullException(nameof(target));
			OperationResult = operationResult;
		}				
				
		public int Total => OperationResult.Total;		

		public override string ToString() => $"DamageEvent [{Source.Name} -> {Target.Name}] : {Total} {Enum.GetName(typeof(DamageType), DamageType)} {Enum.GetName(typeof(DamageSourceType), DamageSourceType)}";
	}
}
