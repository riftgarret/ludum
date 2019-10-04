using System;
using System.Collections.Generic;

namespace Davfalcon
{
	/// <summary>
	/// Manages a group of modifiers. Can function as a single modifier.
	/// </summary>
	[Serializable]
	public class UnitModifierStack : UnitModifier, IUnitModifierStack
	{
		private List<IUnitModifier> stack = new List<IUnitModifier>();

		/// <summary>
		/// Gets the <see cref="IUnit"/> that will be presented as the unit.
		/// </summary>
		protected override IUnit InterfaceUnit => stack.Count > 0 ? stack[stack.Count - 1] : Target;

		/// <summary>
		/// Gets the number of modifiers in the stack.
		/// </summary>
		public int Count => stack.Count;

		/// <summary>
		/// Binds the stack to an <see cref="IUnit"/>.
		/// </summary>
		/// <param name="target">The <see cref="IUnit"/> to bind the stack to.</param>
		public override void Bind(IUnit target)
		{
			// Set target
			base.Bind(target);

			BindStack();
		}

		private void BindStack()
		{
			if (stack.Count > 0)
			{
				// Bind first modifier in the stack to the target
				stack[0].Bind(Target);

				// Rebind rest of stack
				for (int i = 1; i < stack.Count; i++)
				{
					stack[i].Bind(stack[i - 1]);
				}
			}
		}

		/// <summary>
		/// Adds an <see cref="IUnitModifier"/> to the stack.
		/// </summary>
		/// <param name="item">The <see cref="IUnitModifier"/> to be added to the stack.</param>
		public void Add(IUnitModifier item)
		{
			item.Bind(InterfaceUnit);
			stack.Add(item);
		}

		/// <summary>
		/// Removes a specific <see cref="IUnitModifier"/> from the stack.
		/// </summary>
		/// <param name="item">The <see cref="IUnitModifier"/> to remove from the stack.</param>
		/// <returns></returns>
		public bool Remove(IUnitModifier item)
		{
			// Search for referenced item
			int index = stack.FindIndex(X => X.Equals(item));

			// Not found
			if (index == -1) return false;

			RemoveAt(index);
			return true;
		}

		/// <summary>
		/// Removes the <see cref="IUnitModifier"/> at the specified index of the stack.
		/// </summary>
		/// <param name="index">The zero-based index of the <see cref="IUnitModifier"/> to remove.</param>
		public void RemoveAt(int index)
		{
			stack.RemoveAt(index);
			BindStack();
		}

		/// <summary>
		/// Removes all modifiers from the stack.
		/// </summary>
		public void Clear()	=> stack.Clear();

		#region IEnumerable implementation
		private IEnumerator<IUnitModifier> GetEnumerator()
			=> stack.GetEnumerator();

		IEnumerator<IUnitModifier> IEnumerable<IUnitModifier>.GetEnumerator()
			=> GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
		#endregion
	}
}
