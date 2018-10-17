using System;
using System.Collections.Generic;

namespace Redninja.Presenter
{
	internal class ProcessingQueue<T>
	{
		private readonly Queue<T> queue = new Queue<T>();
		private readonly Action<T> func;

		public ProcessingQueue(Action<T> processingFunc) => func = processingFunc;

		public void Enqueue(T item) => queue.Enqueue(item);

		public void Process() => ProcessWhile(() => true);

		public void ProcessWhile(Func<bool> checkCondition)
		{
			while (checkCondition() && queue.Count > 0)
			{
				func(queue.Dequeue());
			}
		}
	}
}
