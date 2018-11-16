using System;
using System.Diagnostics;
using NUnit.Framework;
using Redninja.Logging;

namespace Redninja.UnitTests
{
	[SetUpFixture]
	public class GlobalEnvironment
	{
		[OneTimeSetUp]
		public void InitLogger() {
			RLog.AppendPrinter((string tag, object msg, RLog.LogType logtype) =>
			{
				string text = $"[{tag}] {msg}";
				switch (logtype)
				{
					case RLog.LogType.ERROR:
						Debug.Fail(text);
						break;
					
					default:
						Debug.WriteLine(text);
						break;
				}
			});
		}
	}
}
