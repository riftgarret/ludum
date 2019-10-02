using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davfalcon.Collections.Adapters;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class AdapterTests
	{
		private enum TestEnum { A, B }

		[TestMethod]
		public void ManagedEnumStringList()
		{
			ManagedEnumStringList list = new ManagedEnumStringList
			{
				TestEnum.A,
				TestEnum.B
			};
			Assert.AreEqual(TestEnum.A, list.AsReadOnly()[0]);
		}
	}
}
