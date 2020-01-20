using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.StatCalculators;

namespace Redninja.Components.Combat.Events
{
	public class SkillOperationResult : OperationResult
	{
		public enum Property
		{			
			DamageRaw,
			DamageScale,
			Penetration,
			Reduction,						
			Resistance
		}

		public enum EvalProperty
		{
			Damage,
			Reduction,
			Penetration,
			Resistance,			
		}

		public DamageType DamageType { get; set; }

		public int Damage { get => Historian.Stats[EvalProperty.Damage]; }
		public int Reduction { get => Historian.Stats[EvalProperty.Reduction]; }
		public int Penetration { get => Historian.Stats[EvalProperty.Penetration]; }
		public int Resistance { get => Historian.Stats[EvalProperty.Resistance]; }

		public int Total { get; private set; }

		public DamageSourceType SourceType => DamageSourceType.Skill;

		public EventHistorian Historian { get; }

		public SkillOperationResult(DamageType type, EventHistorian historian)
		{
			Historian = historian;
			DamageType = type;

			historian.RegisterEvaluator(EvalProperty.Damage, stats => (int) (stats[Property.DamageRaw] * stats.AsScalor(Property.DamageScale)));
			historian.RegisterEvaluator(EvalProperty.Penetration, stats => stats[Property.Penetration]);
			historian.RegisterEvaluator(EvalProperty.Resistance, stats => stats[Property.Resistance]);
			historian.RegisterEvaluator(EvalProperty.Reduction, stats => Math.Min(GameRules.MAX_REDUCTION, stats[Property.Reduction]));
			historian.CalculateEvalators();
									
			float calculatedReduction = Math.Max(0, Reduction - Penetration) / 100f;

			Total = (int) Math.Max(0, Damage * (1f - calculatedReduction) - Resistance);			
		}
	}
}
