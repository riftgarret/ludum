namespace Davfalcon.Revelator.Combat
{
	public class LogEntry : ILogEntry
	{
		private string entry;

		public LogEntry(string message)
		{
			entry = message;
		}

		public override string ToString()
		{
			return entry;
		}
	}
}
