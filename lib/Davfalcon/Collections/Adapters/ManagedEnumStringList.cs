using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;
using Davfalcon.Serialization;

namespace Davfalcon.Collections.Adapters
{
	/// <summary>
	/// Derivative of <see cref="ManagedList{EnumString}"/>.
	/// </summary>
	[Serializable]
	public class ManagedEnumStringList : ManagedList<EnumString>
	{
		[NonSerialized]
		private IList<Enum> readOnly;

		/// <summary>
		/// Gets the read-only collection wrapper for the <see cref="List{EnumString}"/> as an <see cref="IList{Enum}"/> interface.
		/// </summary>
		/// <returns></returns>
		new public IList<Enum> AsReadOnly()
		{
			if (readOnly == null)
				readOnly = new EnumStringListAdapter(base.AsReadOnly());
			return readOnly;
		}
	}
}
