using Davfalcon.Revelator;

namespace Redninja.Components.Classes
{
	public interface ICharacteristicRequirement
	{
		bool IsAvailable(IUnit unit);
	}
}
