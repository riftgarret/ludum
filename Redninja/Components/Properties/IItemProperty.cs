using System;
namespace Redninja.Components.Properties
{
	public interface IItemProperty
	{
		string PropertyKey { get; }
		string DisplayName { get; }
		string DisplayDescription { get; }
	}
}
