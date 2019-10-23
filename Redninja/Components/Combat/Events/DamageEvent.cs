using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Combat.Events
{
	public class DamageEvent
	{		
		private IDictionary<DamageType, DamageResult> results = new Dictionary<DamageType, DamageResult>();

		public DamageResult this[DamageType type]
		{
			get => results.ContainsKey(type)? results[type] : null;
		}

		public void PutResult(DamageType type, DamageResult result) => results[type] = result;

		public int Total { get
			{
				return 0;
			} }
	}
}
