namespace Redninja.Logging
{
	public class RLog
    {
        public const string GLOBAL_TAG = "global";

        public delegate void Printer(string tag, object msg, LogType logtype);

        private static event Printer _printers;

        public enum LogType
        {
            DEBUG,
            ERROR
        }

        public static void AppendPrinter(Printer printer)
        {
            _printers += printer;
        }

        public static void D(object tag, object msgObject) 
            => Print(tag is string? (string) tag : tag.GetType().Name, msgObject, LogType.DEBUG);

        public static void E(object tag, object msgObject)
            => Print(tag is string ? (string)tag : tag.GetType().Name, msgObject, LogType.ERROR);

        private static void Print(string tag, object msgObject, LogType logType)
            => _printers?.Invoke(tag, msgObject, logType);
    }
}
