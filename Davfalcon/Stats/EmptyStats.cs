using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davfalcon.Stats
{
	public class EmptyStats : IStats
	{
		public static IStats INSTANCE = new EmptyStats();

		private EmptyStats() { }
		
		public int this[Enum stat] => 0;

		public IEnumerable<Enum> StatKeys => Enumerable.Empty<Enum>();

		public int Get(Enum stat) => 0;
	}
}
