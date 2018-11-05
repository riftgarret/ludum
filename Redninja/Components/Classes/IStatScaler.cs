using System;
namespace Redninja.Components.Classes
{
	public interface IStatScaler
	{
		Func<int, int, int> this[Enum key] { get; }
	}
}
