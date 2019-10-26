using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja
{
	public class LiveStatContainer
	{
		private int current;

		public int Max { get; set; }

		public int Current {
			get => current;
			set => current = Math.Max(0, Math.Min(value, Max));
		}
		public float Percent { get => Max > 0? (Current / (float)Max) : 0; }
	}
}
