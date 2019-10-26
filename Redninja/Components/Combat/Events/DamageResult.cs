using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Combat.Events
{
	public class DamageResult
	{
		public int Damage { get; private set; }
		public int Reduction { get; private set; }
		public int Penetration { get; private set; }
		public int Resistance { get; private set; }
		public int Total { get; private set; }

		public static DamageResult Create(int damage, int reduction, int penetration, int resistance)
		{
			DamageResult result = new DamageResult();
			result.Damage = damage;
			result.Penetration = penetration;
			result.Reduction = reduction;
			result.Resistance = resistance;

			float calculatedReduction = Math.Max(0, reduction - penetration) / 100f;

			result.Total = (int) Math.Max(0, damage * (1f - calculatedReduction) - resistance);
			return result;
		}
	}
}
