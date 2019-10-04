using System;
using System.Collections;
using System.Collections.Generic;
using Davfalcon.Serialization;

namespace Davfalcon.Collections.Adapters
{
	/// <summary>
	/// An adapter to allow representation of an <see cref="IList{EnumString}"/> interface as an <see cref="IList{Enum}"/> interface.
	/// </summary>
	[Serializable]
	public class EnumStringListAdapter : IList<Enum>
	{
		private readonly IList<EnumString> list;

		Enum IList<Enum>.this[int index]
		{
			get => list[index];
			set => list[index] = value;
		}

		int ICollection<Enum>.Count => list.Count;
		bool ICollection<Enum>.IsReadOnly => list.IsReadOnly;

		void ICollection<Enum>.Add(Enum item)
			=> list.Add(item);

		void ICollection<Enum>.Clear()
			=> list.Clear();

		bool ICollection<Enum>.Contains(Enum item)
			=> list.Contains(item);

		void ICollection<Enum>.CopyTo(Enum[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException("array is null.");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException("arrayIndex is less than 0.");
			if (array.Length - arrayIndex < list.Count)
				throw new ArgumentException("The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");
			for (int i = 0; i < list.Count; i++)
			{
				array[arrayIndex + i] = list[i];
			}
		}

		private IEnumerator<Enum> GetEnumerator()
			=> new EnumeratorAdapter(this);

		IEnumerator<Enum> IEnumerable<Enum>.GetEnumerator()
			=> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		int IList<Enum>.IndexOf(Enum item)
			=> list.IndexOf(item);

		void IList<Enum>.Insert(int index, Enum item)
			=> list.Insert(index, item);

		bool ICollection<Enum>.Remove(Enum item)
			=> list.Remove(item);

		void IList<Enum>.RemoveAt(int index)
			=> list.RemoveAt(index);

		/// <summary>
		/// Initializes a new instance of the <see cref="EnumStringListAdapter"/> class for the specified list.
		/// </summary>
		/// <param name="source">The list to be adapted.</param>
		public EnumStringListAdapter(IList<EnumString> source)
			=> list = source;

		private class EnumeratorAdapter : IEnumerator<Enum>
		{
			private readonly IList<Enum> list;
			private int index;

			public EnumeratorAdapter(IList<Enum> list)
			{
				this.list = list;
				Reset();
			}

			public Enum Current => index < 0 ? default : list[index];
			object IEnumerator.Current => Current;

			void IDisposable.Dispose() { }

			public bool MoveNext()
			{
				index++;
				return index < list.Count;
			}

			public void Reset()
				=> index = -1;
		}
	}
}
