﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.AI
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
		private double totalWeight = 0;

		public void Add(T item, double weight)
		{
			if (items.First(x => x.Item1.Equals(item)) != null) throw new InvalidOperationException($"Cannot add the same item: {item}");
			items.Add(new Tuple<T, double>(item, weight));
			totalWeight += weight;
		}

		public void Remove(T item)
		{
			Tuple<T, double> removed = items.First(x => x.Item1.Equals(item));
			items.Remove(removed);
			totalWeight -= removed.Item2;
		}

		public int Count() => items.Count();		

		// for testing
		public T FixedValue(double value) => Select(Math.Min(1, Math.Max(0, value)));

		public T Random() => Select(random.NextDouble());		

		private T Select(double normalizedValue)
		{
			double resultValue = totalWeight * normalizedValue;
			double cursor = 0;
			return items.First(x => {
				cursor += x.Item2;
				return resultValue <= cursor;
			}).Item1;
		}
	}
}