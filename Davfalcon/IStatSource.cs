using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davfalcon
{
	/// <summary>
	/// This represents the edge / leaf node of a stats tree. From here we provide
	/// the value and name of the stats being used.
	/// </summary>
	public interface IStatSource
	{
		string Name { get; }
		IStats Stats { get; }
	}
}
