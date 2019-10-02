using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davfalcon.Serialization;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class EnumStringTests
	{
		private enum TestEnum
		{
			A, B, C
		}

		private enum OtherTestEnum
		{
			A, B
		}

		[TestMethod]
		new public void ToString()
		{
			EnumString a = new EnumString(TestEnum.A);
			Assert.AreEqual("A", a.ToString());
		}

		[TestMethod]
		public void Equals()
		{
			EnumString a = new EnumString(TestEnum.A);
			EnumString aa = new EnumString(OtherTestEnum.A);
			Assert.AreEqual(a, TestEnum.A);
			Assert.AreEqual(a, "A");
			Assert.IsFalse(a.Equals(aa));
		}

		[TestMethod]
		public void ImplicitConversionFromEnum()
		{
			EnumString a = TestEnum.A;
			Assert.IsTrue(a.Equals(TestEnum.A));
		}

		[TestMethod]
		public void ImplicitConversionToEnum()
		{
			EnumString a = TestEnum.A;
			Enum _a = a;
			Assert.AreEqual(TestEnum.A, _a);
		}

		[TestMethod]
		public void ImplicitConversionToString()
		{

			EnumString a = TestEnum.A;
			string s = a;
			Assert.IsTrue(a == "A");
			Assert.IsTrue("A" == a);
			Assert.AreEqual(a, s);
		}

		[TestMethod]
		public void HashCode()
		{
			EnumString a = new EnumString(TestEnum.A);
			Dictionary<EnumString, object> dict = new Dictionary<EnumString, object> { [a] = null };
			dict[TestEnum.B] = TestEnum.B;
			Assert.AreEqual(dict[TestEnum.A], dict[a]);
			Assert.AreEqual(dict[TestEnum.B], TestEnum.B);
		}

		[TestMethod]
		public void EqualityOperator()
		{
			EnumString a = new EnumString(TestEnum.A);
			EnumString b = new EnumString(TestEnum.B);

			Assert.IsTrue(a == TestEnum.A);
			Assert.IsFalse(a == b);
		}

		[TestMethod]
		public void InequalityOperator()
		{
			EnumString a = new EnumString(TestEnum.A);
			EnumString b = new EnumString(TestEnum.B);

			Assert.IsTrue(a != TestEnum.B);
			Assert.IsTrue(a != b);
		}

		[TestMethod]
		public void Serialization()
		{
			EnumString a = new EnumString(TestEnum.A);
			EnumString clone = Serializer.DeepClone(a);

			Assert.AreEqual(a, clone);
			Assert.AreEqual(clone, TestEnum.A);
		}

		[TestMethod]
		public void CollectionSerialization()
		{
			EnumString a = new EnumString(TestEnum.A);

			List<EnumString> list = new List<EnumString> { a  };
			List<EnumString> listClone = Serializer.DeepClone(list);

			Assert.AreEqual(list[0], listClone[0]);
		}

		[TestMethod]
		[ExpectedException(typeof(MemberAccessException))]
		public void EnumListDeserialization()
		{
			List<Enum> list = new List<Enum> { TestEnum.A };
			List<Enum> listClone = Serializer.DeepClone(list);
		}


		[TestMethod]
		public void EnumDictionaryDeserialization()
		{
			Dictionary<Enum, Enum> dict = new Dictionary<Enum, Enum> { [TestEnum.A] = OtherTestEnum.B };
			Dictionary<Enum, Enum> clone = Serializer.DeepClone(dict);

			Assert.AreEqual(dict[TestEnum.A], clone[TestEnum.A]);
		}
	}
}
