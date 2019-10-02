using System;

namespace Davfalcon.Revelator
{
	public interface ILinkedStatResolver
	{
		bool Get(Enum stat, out int value);
		void Bind(IUnit unit);
	}
}
