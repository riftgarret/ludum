using System;
using System.Collections.Generic;

namespace Redninja.Presenter
{
	internal class PriorityProcessingQueue<TKey, TValue>
	{
		private readonly SortedList<TKey, TValue> list = new SortedList<TKey, TValue>();
		private readonly Action<TValue> func;

		public PriorityProcessingQueue(Action<TValue> processingFunc) => func = processingFunc;

		public void Enqueue(TKey priorityIndex, TValue item) => list.Add(priorityIndex, item);

		public void Process() => ProcessWhile(() => true);

		public void ProcessWhile(Func<bool> checkCondition)
		{
			while (checkCondition() && list.Count > 0)
			{
				TValue item = list.Values[0];
				list.RemoveAt(0);
				func(item);
			}
		}
	}
}
