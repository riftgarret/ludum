using Davfalcon.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class ManagedListTests
	{
		[TestMethod]
		public void AsReadOnly()
		{
			ManagedList<int> list = new ManagedList<int>();
			list.Add(1);

			Assert.AreEqual(list.AsReadOnly(), list.AsReadOnly());
			Assert.AreEqual(1, list.AsReadOnly()[0]);
			Assert.IsTrue(list.AsReadOnly().IsReadOnly);
		}
	}
}
