using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davfalcon
{
	public interface IStatsProvider
	{	
		/// <summary>
		/// Gets the source of where some stats originated from.
		/// </summary>
		/// <param name="stat"></param>
		/// <returns></returns>
		IEnumerable<IStatSource> GetSources(Enum stat);

		/// <summary>
		/// Get all sources.
		/// </summary>
		/// <returns></returns>
		IEnumerable<IStatSource> AllSources();

		/// <summary>
		/// Raw stats for getting values
		/// </summary>
		IStats Stats { get; }
	}
}
