using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Decisions.AI
{
	/// <summary>
	/// Weighted pool is a set of items with a weight. You can pick an
	/// item at random or by forcing a value if testing or any particular reason.
	/// 
	/// Warning, current implementation is not meant for large lists.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class WeightedPool<T>
	{
		private Random random = new Random();
		private List<Tuple<T, double>> items = new List<Tuple<T, double>>();
		public double TotalWeight { get; private set; } = 0;

		public void Add(T item, double weight)
		{
			if (items.Any(x => x.Item1.Equals(item))) throw new InvalidOperationException($"Cannot add the same item: {item}");
			items.Add(Tuple.Create(item, weight));
			TotalWeight += weight;
		}

		public void Remove(T item)
		{
			Tuple<T, double> removed = items.FirstOrDefault(x => x.Item1.Equals(item));
			items.Remove(removed);
			TotalWeight -= removed.Item2;
		}

		public int Count() => items.Count();		

		// for testing
		internal T FixedValue(double value) => Select(Math.Min(1, Math.Max(0, value))).Item1;

		public T Random() => Select(random.NextDouble()).Item1;

		public Tuple<T, double> RandomResult() => Select(random.NextDouble());

		public IEnumerable<Tuple<T, double>> WeightedItems => items.AsEnumerable();

		private Tuple<T, double> Select(double normalizedValue)
		{
			double resultValue = TotalWeight * normalizedValue;
			double cursor = 0;
			return Tuple.Create(items.FirstOrDefault(x => {
				cursor += x.Item2;
				return resultValue <= cursor;
			}).Item1, resultValue);
		}
	}
}
