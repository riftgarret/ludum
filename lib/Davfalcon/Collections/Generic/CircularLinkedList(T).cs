using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Davfalcon.Collections.Generic
{
	/// <summary>
	/// Represents a list that can be circularly rotated in one direction.
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	[Serializable]
	public class CircularLinkedList<T> : ICircularLinkedList<T>
	{
		private List<T> list = new List<T>();
		private int head = 0;

		private int GetActualIndexFromOffset(int offsetIndex)
		{
			return (head + offsetIndex) % Count;
		}

		private int GetOffsetIndexFromActual(int actualIndex)
		{
			return (actualIndex - head) % Count;
		}

		private void ThrowIfIndexOutOfRange(int index)
		{
			if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException();
		}

		/// <summary>
		/// Gets the object currently at the head of the list.
		/// </summary>
		public T Current
		{
			get { return list[head]; }
		}

		/// <summary>
		/// Moves the head of the list forward by one element.
		/// </summary>
		public void Rotate()
		{
			Rotate(1);
		}

		/// <summary>
		/// Moves the head of the list forward by the specified number of elements.
		/// </summary>
		/// <param name="steps">Number of elements to move the head.</param>
		public void Rotate(int steps)
		{
			if (Count > 1)
				head = GetActualIndexFromOffset(steps);
		}

		/// <summary>
		/// Searches for the specified object and returns the zero-based index of the first occurrence relative to the current head.
		/// </summary>
		/// <param name="item">The object to locate.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="item"/> relative to the current head, if found; otherwise, -1.</returns>
		public int IndexOf(T item)
		{
			int index = list.IndexOf(item, head);
			if (head > 0 && index < 0)
			{
				index = list.IndexOf(item, 0, head);
			}
			return index < 0 ? index : GetOffsetIndexFromActual(index);
		}

		/// <summary>
		/// Inserts an element at the specified index relative to the current head.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted relative to the current head.</param>
		/// <param name="item">The object to insert.</param>
		public void Insert(int index, T item)
		{
			ThrowIfIndexOutOfRange(index);
			int actual = GetActualIndexFromOffset(index);
			list.Insert(actual, item);
			if (actual < head) head++;
		}

		/// <summary>
		/// Removes the element at the specified index relative to the current head.
		/// </summary>
		/// <param name="index">The zero-based index of the element to remove relative to the current head.</param>
		public void RemoveAt(int index)
		{
			ThrowIfIndexOutOfRange(index);
			int actual = GetActualIndexFromOffset(index);
			list.RemoveAt(actual);
			if (actual < head) head--;
		}

		/// <summary>
		/// Gets or sets the element at the specified index relative to the current head.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get or set relative to the current head.</param>
		/// <returns>The element at the specified index.</returns>
		public T this[int index]
		{
			get
			{
				ThrowIfIndexOutOfRange(index);
				return list[GetActualIndexFromOffset(index)];
			}
			set
			{
				ThrowIfIndexOutOfRange(index);
				list[GetActualIndexFromOffset(index)] = value;
			}
		}

		/// <summary>
		/// Adds an object to the end of the current list order.
		/// </summary>
		/// <param name="item">The object to be added to the end of the list.</param>
		public void Add(T item)
		{
			list.Insert(head, item);
			head++;
		}

		/// <summary>
		/// Removes all elements from the list and resets the head.
		/// </summary>
		public void Clear()
		{
			list.Clear();
			head = 0;
		}

		/// <summary>
		/// Determines whether an element is in the list.
		/// </summary>
		/// <param name="item">The object to locate in the list.</param>
		/// <returns><c>true</c> if <paramref name="item"/> is found in the list; otherwise, <c>false</c>.</returns>
		public bool Contains(T item)
		{
			return list.Contains(item);
		}

		/// <summary>
		/// Copies the current list to a compatible one-dimensional array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the list.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			for (int i = 0; i < Count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}

		/// <summary>
		/// Gets the number of elements in the list.
		/// </summary>
		public int Count
		{
			get { return list.Count; }
		}
		
		bool ICollection<T>.IsReadOnly
		{
			get { return false; }
		}

		/// <summary>
		/// Removes the first occurrence of a specific object relative to the current head.
		/// </summary>
		/// <param name="item">The object to remove from the list.</param>
		/// <returns><c>true</c> if <paramref name="item"/> is successfully removed; otherwise, <c>false</c>. This method also returns <c>false</c> if <paramref name="item"/> was not found.</returns>
		public bool Remove(T item)
		{
			int index = list.IndexOf(item);
			if (index >= 0)
			{
				list.RemoveAt(index);
				if (index < head) head--;
				return true;
			}
			else
			{
				return false;
			}
		}

		private IEnumerator<T> GetEnumerator()
		{
			return new CLLEnumerator(this);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Returns a read-only <see cref="ReadOnlyCollection{T}"/> wrapper for the current collection.
		/// </summary>
		/// <returns>An object that acts as a read-only wrapper around the current list.</returns>
		public ReadOnlyCollection<T> AsReadOnly()
		{
			return new ReadOnlyCollection<T>(this);
		}

		/// <summary>
		/// Sorts the elements in the list using the default comparer and resets the head.
		/// </summary>
		public void Sort()
		{
			list.Sort();
			head = 0;
		}

		/// <summary>
		/// Sorts the elements in the list using the specified <see cref="Comparison{T}"/> and resets the head.
		/// </summary>
		/// <param name="comparison"></param>
		public void Sort(Comparison<T> comparison)
		{
			list.Sort(comparison);
			head = 0;
		}

		/// <summary>
		/// Sorts the elements using the specified comparer and resets the head.
		/// </summary>
		/// <param name="comparer"></param>
		public void Sort(IComparer<T> comparer)
		{
			list.Sort(comparer);
			head = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CircularLinkedList{T}"/> class that is empty.
		/// </summary>
		public CircularLinkedList() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="CircularLinkedList{T}"/> class and adds the specified item to it.
		/// </summary>
		/// <param name="item">The item to be added to the list.</param>
		public CircularLinkedList(T item)
		{
			list.Add(item);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CircularLinkedList{T}"/> class that contains elements copied from the specified collection.
		/// </summary>
		/// <param name="items">The collection whose elements are copied to the new <see cref="CircularLinkedList{T}"/>.</param>
		public CircularLinkedList(IEnumerable<T> items)
		{
			list = new List<T>();
			foreach (T item in items)
			{
				list.Add(item);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CircularLinkedList{T}"/> class that uses the specified <see cref="List{T}"/> object as a base.
		/// </summary>
		/// <param name="list">The <see cref="List{T}"/> to use as a base for the new <see cref="CircularLinkedList{T}"/>.</param>
		/// <returns>A new <see cref="CircularLinkedList{T}"/> with <paramref name="list"/> as the base.</returns>
		public static CircularLinkedList<T> WrapList(List<T> list)
		{
			CircularLinkedList<T> cll = new CircularLinkedList<T> { list = list };
			return cll;
		}

		private class CLLEnumerator : IEnumerator<T>
		{
			private CircularLinkedList<T> list;
			private int curIndex;

			public CLLEnumerator(CircularLinkedList<T> list)
			{
				this.list = list;
				Reset();
			}

			public T Current
			{
				get { return curIndex < 0 ? default : list[curIndex]; }
			}

			void IDisposable.Dispose() { }

			object IEnumerator.Current
			{
				get { return Current; }
			}

			public bool MoveNext()
			{
				curIndex++;
				return curIndex < list.Count;
			}

			public void Reset()
			{
				curIndex = -1;
			}
		}
	}
}
