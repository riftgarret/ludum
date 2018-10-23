namespace Redninja.System
{
	public interface ISystemProvider
	{
		IClassProvider GetClass(string className);
	}
}
