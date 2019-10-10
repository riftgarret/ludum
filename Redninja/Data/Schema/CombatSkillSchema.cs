using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Redninja.Components.Skills;

namespace Redninja.Data.Schema
{
	[Serializable]
	internal class CombatSkillSchema : IDataSource
	{
		public string DataId { get; set; }

		public string Name { get; set; }

		public List<float> Time { get; set; }

		public List<TargetingSetSchema> TargetingSets { get; set; }

		public CombatRoundParametersSchema DefaultParameters { get; set; }

		[OnDeserialized]
		private void SetDefaultParameters(StreamingContext context)
		{
			if (DefaultParameters == null)
				DefaultParameters = new CombatRoundParametersSchema()
				{
					Name = Name,
					DamageTypes = new List<DamageType>()
				};
			else DefaultParameters.Name = Name;

			foreach (TargetingSetSchema ts in TargetingSets)
			{
				foreach (CombatRoundSchema cr in ts.CombatRounds)
				{
					if (cr.Parameters != null)
						cr.Parameters.Name = Name;
				}
			}
		}
	}

	[Serializable]
	internal class TargetingSetSchema
	{
		public string TargetingRuleId { get; set; }
		public List<CombatRoundSchema> CombatRounds { get; set; }
	}

	[Serializable]
	internal class CombatRoundSchema
	{
		public float ExecutionStart { get; set; }
		public string OperationProviderName { get; set; }
		public string Pattern { get; set; }
		public CombatRoundParametersSchema Parameters { get; set; }
	}

	[Serializable]
	internal class CombatRoundParametersSchema : ISkillOperationParameters
	{
		public string Name { get; set; }
		public int BaseDamage { get; set; }
		public int CritMultiplier { get; set; }
		public Stat? BonusDamageStat { get; set; }
		Enum IDamageSource.BonusDamageStat => BonusDamageStat;
		public List<DamageType> DamageTypes { get; set; }
		IEnumerable<Enum> IDamageSource.DamageTypes => DamageTypes.Select(t => t as Enum);
	}
}
