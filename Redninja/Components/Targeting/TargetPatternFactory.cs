using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Targeting
{
	/// <summary>
	/// Helper method for building out patterns.
	/// </summary>
	public class TargetPatternFactory
	{
		public static ITargetPattern CreateRowPattern() => CreateRowPattern(0);
		public static ITargetPattern CreateRowPattern(int offset) => new EvalfPattern($"row{offset}", (ar, ac, tr, tc) => tr == ar + offset);
		public static ITargetPattern CreateColumnPattern() => CreateColumnPattern(0);
		public static ITargetPattern CreateColumnPattern(int offset) => new EvalfPattern($"col{offset}", (ar, ac, tr, tc) => tc == ac + offset);
		public static ITargetPattern CreatePattern(params Coordinate[] tiles)
		{
			List<Coordinate> list = new List<Coordinate>(tiles);
			return new EvalfPattern(String.Join(" ", tiles),
				(ar, ac, tr, tc) => list.Select(c => new Coordinate(c.Row + ar, c.Column + ac)).Contains(new Coordinate(tr, tc)));
		}

		private class EvalfPattern : ITargetPattern
		{
			public delegate bool EvalPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn);

			private readonly string identifier;
			private readonly EvalPattern EvalPatternDelegate;

			public EvalfPattern(string identifier, EvalPattern evalPattern)
			{
				this.identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
				EvalPatternDelegate = evalPattern ?? throw new ArgumentNullException(nameof(EvalPatternDelegate));
			}

			public bool ContainsLocation(int anchorRow, int anchorColumn, int targetRow, int targetColumn)
				=> EvalPatternDelegate(anchorRow, anchorColumn, targetRow, targetColumn);

			public bool ContainsLocation(Coordinate anchor, int targetRow, int targetColumn)
				=> ContainsLocation(anchor.Row, anchor.Column, targetRow, targetColumn);

			public bool ContainsLocation(Coordinate anchor, Coordinate target)
				=> ContainsLocation(anchor.Row, anchor.Column, target.Row, target.Column);

			public override bool Equals(object obj) => obj is EvalfPattern p && p.identifier == identifier;

			public override int GetHashCode() => identifier.GetHashCode();
		}
	}
}
