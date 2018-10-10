using System.Collections.Generic;

namespace Redninja.Targeting
{
	/// <summary>
	/// Pattern to define whether a unit lays within it.
	/// </summary>
	public interface ITargetPattern
	{
		/// <summary>
		/// Does this entity lay within this pattern.
		/// </summary>
		/// <param name="anchorRow">Row where pattern originates</param>
		/// <param name="anchorColumn">Column where pattern originates</param>
		/// <param name="targetRow">Tested Row</param>
		/// <param name="targetColumn">Tested Column</param>
		/// <returns></returns>
		bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn);
		bool IsInPattern(Coordinate anchor, Coordinate target);
	}
}
