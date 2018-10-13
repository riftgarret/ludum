using NUnit.Framework;
using Redninja.AI;

namespace Redninja.UnitTests.AI
{
	[TestFixture]
	public class WeightedPoolTests
	{
		private WeightedPool<object> subject;

		[SetUp]
		public void Setup() => subject = new WeightedPool<object>();

		private object Any() => new object();

		[Test]
		public void Add_WeightUpdated()
		{
			subject.Add(Any(), 5);

			Assert.That(subject.TotalWeight, Is.EqualTo(5));

			subject.Add(Any(), 1);

			Assert.That(subject.TotalWeight, Is.EqualTo(6));
		}

		[Test]
		public void Add_CountUpdated()
		{
			subject.Add(Any(), 5);

			Assert.That(subject.Count(), Is.EqualTo(1));

			subject.Add(Any(), 1);

			Assert.That(subject.Count(), Is.EqualTo(2));
		}

		[TestCase(1, 1)]
		[TestCase(4, 1)]
		[TestCase(4, 2)]
		[TestCase(3, 1)]
		public void Remove_WeightUpdated(int itemCount, int weightSize)
		{
			object[] items = new object[itemCount];

			for (int i = 0; i < itemCount; i++)
			{
				items[i] = Any();
				subject.Add(items[i], weightSize);
			}

			int initialWeight = weightSize * itemCount;

			for (int i = 0; i < itemCount; i++)
			{
				subject.Remove(items[i]);
				Assert.That(subject.TotalWeight, Is.EqualTo(initialWeight - (1 + i) * weightSize));
			}
		}

		[TestCase(1)]
		[TestCase(4)]
		[TestCase(4)]
		[TestCase(3)]
		public void Remove_CountUpdated(int itemCount)
		{
			object[] items = new object[itemCount];

			for (int i = 0; i < itemCount; i++)
			{
				items[i] = Any();
				subject.Add(items[i], 1);
			}

			for (int i = 0; i < itemCount; i++)
			{
				subject.Remove(items[i]);
				Assert.That(subject.Count(), Is.EqualTo(itemCount - (i + 1)));
			}
		}

		[TestCase(0.25, 0, new double[]{0.5, 0.5})]
		[TestCase(0.75, 1, new double[] { 0.5, 0.5 })]
		[TestCase(0.49, 0, new double[] { 0.5, 0.5 })]
		[TestCase(0.9, 3, new double[] { 0.25, 0.25, 0.25, 0.25 })]
		[TestCase(0.9, 3, new double[] { 5, 5, 5, 5 })]
		[TestCase(0.2, 0, new double[] { 5, 5, 5, 5 })]
		[TestCase(0.45, 3, new double[] { 1, 1, 1, 5 })]
		[TestCase(0.45, 0, new double[] { 5, 1, 1, 1 })]
		[TestCase(0.45, 0, new double[] { 5 })]
		public void GetItem_MatchesFixedValue(double forcedWeight, int expectedIndex , double [] weights)
		{
			object[] items = new object[weights.Length];
			for (int i = 0; i < weights.Length; i++)
			{
				items[i] = Any();
				subject.Add(items[i], weights[i]);
			}

			object item = subject.FixedValue(forcedWeight);

			Assert.That(item, Is.EqualTo(items[expectedIndex]));
		}

		[TestCase(0.5, 0, 1, new double[] { 0.5, 0.5 })]
		[TestCase(0.25, 1, 0, new double[] { 0.5, 0.5 })]		
		[TestCase(0.9, 2, 3, new double[] { 0.25, 0.25, 0.25, 0.25 })]
		[TestCase(0.6, 1, 3, new double[] { 5, 5, 5, 5 })]
		[TestCase(0.2, 1, 0, new double[] { 5, 5, 5, 5 })]
		[TestCase(0.3, 3, 0, new double[] { 1, 1, 1, 5 })]
		[TestCase(0.45, 2, 0, new double[] { 5, 1, 1, 1 })]
		public void RemoveItem_GetItem_MatchesFixedValue(double forcedWeight, int expectedIndex, int removeIndex, double[] weights)
		{
			object[] items = new object[weights.Length];
			for (int i = 0; i < weights.Length; i++)
			{
				items[i] = Any();
				subject.Add(items[i], weights[i]);
			}

			subject.Remove(items[removeIndex]);

			object item = subject.FixedValue(forcedWeight);

			Assert.That(item, Is.EqualTo(items[expectedIndex]));
		}
	}
}
