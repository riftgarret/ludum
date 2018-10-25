using System;
namespace Redninja.Components
{
	public interface IItemProperty
	{
		string PropertyKey { get; }
		string DisplayName { get; }
		string DisplayDescription { get; }
	}
}
