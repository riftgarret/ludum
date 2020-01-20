using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.StatCalculators;

namespace Redninja.Components.Combat.Events
{
	public class TickOperationResult : OperationResult
	{
		public enum Property
		{			
			DamageRaw,
			DamageScale,		
			Reduction,									
		}

		public enum EvalProperty
		{
			Damage,
			Reduction,					
		}

		public DamageType DamageType { get; set; }
		public int Damage { get => Historian.Stats[EvalProperty.Damage]; }
		public int Reduction { get => Historian.Stats[EvalProperty.Reduction]; }		

		public int Total { get; private set; }

		public DamageSourceType SourceType => DamageSourceType.Tick;

		public EventHistorian Historian { get; }

		public TickOperationResult(DamageType type, EventHistorian historian)
		{
			Historian = historian;
			DamageType = type;

			historian.RegisterEvaluator(EvalProperty.Damage, stats => (int) (stats[Property.DamageRaw] * stats.AsScalor(Property.DamageScale)));			
			historian.RegisterEvaluator(EvalProperty.Reduction, stats => Math.Min(GameRules.MAX_REDUCTION, stats[Property.Reduction]));
			historian.CalculateEvalators();
									
			float calculatedReduction = Math.Max(0, Reduction) / 100f;

			Total = (int) Math.Max(0, Damage * (1f - calculatedReduction));			
		}
	}
}
