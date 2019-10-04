using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davfalcon.Collections.Generic;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class CircularLinkedListTests
	{
		private CircularLinkedList<int> list;

		[TestInitialize]
		public void CreateList()
		{
			list = new CircularLinkedList<int>();
			list.Add(0);
			list.Add(1);
			list.Add(2);
		}

		[TestMethod]
		public void Add()
		{
			Assert.AreEqual(0, list[0]);
			Assert.AreEqual(1, list[1]);
			Assert.AreEqual(2, list[2]);
		}

		[TestMethod]
		public void Rotate()
		{
			list.Rotate();
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(2, list[1]);
			Assert.AreEqual(0, list[2]);
		}

		[TestMethod]
		public void RotateWrap()
		{
			list.Rotate(5);
			Assert.AreEqual(2, list[0]);
			Assert.AreEqual(0, list[1]);
			Assert.AreEqual(1, list[2]);
		}

		[TestMethod]
		public void InsertAfterRotate()
		{
			list.Rotate();
			list.Insert(1, 3);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(3, list[1]);
			Assert.AreEqual(2, list[2]);
			Assert.AreEqual(0, list[3]);
		}

		[TestMethod]
		public void RemoveAfterRotate()
		{
			list.Rotate();
			list.Remove(1);
			Assert.AreEqual(2, list[0]);
			Assert.AreEqual(0, list[1]);
		}

		[TestMethod]
		public void RemoveAtAfterRotate()
		{
			list.Rotate();
			list.RemoveAt(1);
			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(0, list[1]);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException), "Modification of read-only list was allowed.")]
		public void AsReadOnly()
		{
			IList<int> readOnlyList = list.AsReadOnly();
			list.Rotate();

			Assert.AreEqual(1, list[0]);
			Assert.AreEqual(2, list[1]);
			Assert.AreEqual(0, list[2]);

			readOnlyList.Add(3);
		}

		[TestMethod]
		public void Sort()
		{
			list.Insert(1, 3);
			list.Insert(0, 4);
			list.Rotate();

			list.Sort();
			Assert.AreEqual(0, list[0]);
			Assert.AreEqual(1, list[1]);
			Assert.AreEqual(2, list[2]);
			Assert.AreEqual(3, list[3]);
			Assert.AreEqual(4, list[4]);
		}
	}
}
