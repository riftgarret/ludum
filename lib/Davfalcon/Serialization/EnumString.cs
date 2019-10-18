using System;
using System.Runtime.Serialization;

namespace Davfalcon.Serialization
{
	/// <summary>
	/// Class to serialize enums as strings.
	/// </summary>
	[Serializable]
	public sealed class EnumString
	{
		private readonly string str;
		private readonly Type type;

		/// <summary>
		/// Create a new <see cref="EnumString"/> from the specified enum value.
		/// </summary>
		/// <param name="e">The enum to convert.</param>
		public EnumString(Enum e)
		{
			str = e.ToString();
			type = e.GetType();
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
			=> str.Equals(obj.ToString()) &&
			!(obj is Enum && !type.Equals(obj.GetType())) &&
			!(obj is EnumString && !type.Equals((obj as EnumString).type));

		/// <summary>
		/// A string that represents the enum's value.
		/// </summary>
		/// <returns>The string representation of the enum.</returns>
		public override string ToString()
			=> str;

		/// <summary>
		/// Gets a hash code based on the enum's type and string.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
			=> (type + str).GetHashCode();

		/// <summary>
		/// Converts an enum to an <see cref="EnumString"/>.
		/// </summary>
		/// <param name="e">The enum to convert.</param>
		public static implicit operator EnumString(Enum e)
			=> new EnumString(e);

		/// <summary>
		/// Converts an <see cref="EnumString"/> to an enum.
		/// </summary>
		/// <param name="es">The <see cref="EnumString"/> to convert.</param>
		public static implicit operator Enum(EnumString es)
			=> (Enum)Enum.Parse(es.type, es.str);

		/// <summary>
		/// Converts an <see cref="EnumString"/> to its string representation.
		/// </summary>
		/// <param name="es">The <see cref="EnumString"/> to convert</param>
		public static implicit operator string(EnumString es)
			=> es.ToString();

		/// <summary>
		/// Determines whether the specified <see cref="EnumString"/> instances are considered equal.
		/// </summary>
		/// <param name="a">The first object to compare.</param>
		/// <param name="b">The second object to compare.</param>
		/// <returns><c>true</c> if the objects are considered equal; otherwise, <c>false</c>.</returns>
		public static bool operator ==(EnumString a, EnumString b)
			=> a.Equals(b);

		/// <summary>
		/// Determines whether the specified <see cref="EnumString"/> instances are considered not equal.
		/// </summary>
		/// <param name="a">The first object to compare.</param>
		/// <param name="b">The second object to compare.</param>
		/// <returns><c>true</c> if the objects are considered not equal; otherwise, <c>false</c>.</returns>
		public static bool operator !=(EnumString a, EnumString b)
			=> !a.Equals(b);

		/// <summary>
		/// Converts an array of enum objects into an array of <see cref="EnumString"/> instances.
		/// </summary>
		/// <param name="array">The array containing the enum objects to convert.</param>
		/// <returns>An array containing the new <see cref="EnumString"/> instances.</returns>
		public static EnumString[] ConvertEnumArray(Enum[] array)
			=> Array.ConvertAll<Enum, EnumString>(array, e => e);
	}
}
