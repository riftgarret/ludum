using System;
using System.Collections.Generic;

namespace Davfalcon.Collections.Generic
{
	/// <summary>
	/// Convenience class to avoid repeated calls to <see cref="List{T}.AsReadOnly"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class ManagedList<T> : List<T>
	{
		[NonSerialized]
		private IList<T> readOnly;

		/// <summary>
		/// Gets the read-only collection wrapper for the <see cref="List{T}"/>.
		/// </summary>
		new public IList<T> AsReadOnly()
		{
			if (readOnly == null)
				readOnly = base.AsReadOnly();
			return readOnly;
		}
	}
}
