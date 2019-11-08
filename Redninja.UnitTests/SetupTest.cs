using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Redninja.Logging;

namespace Redninja
{
	[SetUpFixture]
	public class SetupTest
	{
		[OneTimeSetUp]
		public void HookRLog()
		{
			RLog.AppendPrinter((tag, msg, logtype) => Debug.WriteLine($"{tag}: {msg}"));
		}

	}
}
