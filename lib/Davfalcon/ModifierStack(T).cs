using System;
using System.Collections.Generic;

namespace Davfalcon
{
	/// <summary>
	/// A collection of modifiers that can be treated as a single modifier.
	/// </summary>
	[Serializable]
	public class ModifierStack<T> : Modifier<T>, IModifierStack<T> where T : class
	{
		private readonly List<IModifier<T>> stack = new List<IModifier<T>>();

		/// <summary>
		/// Gets a representation of the modified object with all modifiers applied.
		/// </summary>
		public override T AsModified() => stack.LastOrDefault()?.AsModified() ?? Target;

		/// <summary>
		/// Gets the number of modifiers in the stack.
		/// </summary>
		public int Count => stack.Count;

		/// <summary>
		/// Gets or sets the modifier at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the modifier to get or set.</param>
		/// <returns>The modifier at the specified index.</returns>
		public IModifier<T> this[int index]
		{
			get => stack[index];
			set
			{
				stack[index] = value;
				RebindAtIndex(index);
			}
		}

		/// <summary>
		/// Binds the stack to an object.
		/// </summary>
		/// <param name="target">The entity to bind the stack to.</param>
		public override void Bind(T target)
		{
			// Set target
			base.Bind(target);

			// Rebind the entire stack in case of deserialization
			BindStack();
		}

		/// <summary>
		/// Removes all modifiers from the stack.
		/// </summary>
		public void Clear() => stack.Clear();

		/// <summary>
		/// Determines the index of a specific modifier in the stack.
		/// </summary>
		/// <param name="item">The modifier to locate in the stack.</param>
		/// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
		public int IndexOf(IModifier<T> item) => stack.IndexOf(item);

		/// <summary>
		/// Inserts a modifier at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
		/// <param name="item">The modifier to insert into the stack.</param>
		public void Insert(int index, IModifier<T> item)
		{
			stack.Insert(index, item);
			RebindAtIndex(index);
		}

		/// <summary>
		/// Adds a modifier to the stack.
		/// </summary>
		/// <param name="item">The modifier to add to the stack.</param>
		public void Add(IModifier<T> item)
		{
			stack.Add(item);
			RebindAtIndex(Count - 1);
		}

		/// <summary>
		/// Determines whether a specific modifier is in the stack.
		/// </summary>
		/// <param name="item">The modifier to locate in the stack.</param>
		/// <returns><c>true</c> if <paramref name="item"/> is found in the stack; otherwise, <c>false</c>.</returns>
		public bool Contains(IModifier<T> item) => stack.Contains(item);

		/// <summary>
		/// Removes a specific modifier from the stack.
		/// </summary>
		/// <param name="item">The modifier to remove from the stack.</param>
		/// <returns><c>true</c> if <paramref name="item"/> was successfully removed from the stack; otherwise, <c>false</c>.</returns>
		public bool Remove(IModifier<T> item)
		{
			int index = stack.FindIndex(m => m.Equals(item));

			if (index == -1) return false;

			RemoveAt(index);
			return true;
		}

		/// <summary>
		/// Removes the modifier at the specified index in the stack.
		/// </summary>
		/// <param name="index">The zero-based index of the modifier to remove.</param>
		public void RemoveAt(int index)
		{
			stack.RemoveAt(index);

			if (Count > index)
			{
				RebindAtIndex(index);
			}
		}

		bool ICollection<IModifier<T>>.IsReadOnly => false;
		void ICollection<IModifier<T>>.CopyTo(IModifier<T>[] array, int arrayIndex) => stack.CopyTo(array, arrayIndex);

		private IEnumerator<IModifier<T>> GetEnumerator() => stack.GetEnumerator();
		IEnumerator<IModifier<T>> IEnumerable<IModifier<T>>.GetEnumerator() => GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		private void BindStack()
		{
			if (stack.Count > 0)
			{
				// Bind first modifier in the stack to the target
				stack[0].Bind(Target);

				// Rebind rest of stack
				for (int i = 1; i < stack.Count; i++)
				{
					stack[i].Bind(stack[i - 1].AsModified);
				}
			}
		}

		private void RebindAtIndex(int index)
		{
			IModifier<T> item = stack[index];

			if (index == 0)
			{
				item.Bind(Target);
			}
			else
			{
				item.Bind(stack[index - 1].AsModified);
			}

			if (Count > index + 1)
			{
				stack[index + 1].Bind(item.AsModified);
			}
		}
	}
}
