using System.Collections.Generic;

namespace Davfalcon.Collections.Generic
{
	/// <summary>
	/// Represents a list of objects with a head element that can be circularly rotated.
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	public interface ICircularLinkedList<T> : IList<T>
	{
		/// <summary>
		/// Gets the object currently at the head of the list.
		/// </summary>
		T Current { get; }

		/// <summary>
		/// Moves the head of the list forward by one element.
		/// </summary>
		void Rotate();

		/// <summary>
		/// Moves the head of the list forward by the specified number of elements.
		/// </summary>
		/// <param name="steps">Number of elements to move the head.</param>
		void Rotate(int steps);
	}
}
