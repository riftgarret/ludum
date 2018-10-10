using System;
using System.Collections.Generic;
using System.Linq;

namespace Redninja.Targeting
{
	/// <summary>
	/// Helper method for building out patterns.
	/// </summary>
	public class TargetPatternFactory
	{
		public static ITargetPattern CreateRowPattern() => new EvalfPattern((ar, ac, tr, tc) => tr == ar);
		public static ITargetPattern CreateColumnPattern() => new EvalfPattern((ar, ac, tr, tc) => tc == ac);
		public static ITargetPattern CreatePattern(params Coordinate[] tiles)
		{
			List<Coordinate> list = new List<Coordinate>(tiles);
			return new EvalfPattern((ar, ac, tr, tc) => list.Select(c => new Coordinate(c.Row + ar, c.Column + ac)).Contains(new Coordinate(tr, tc)));
		}

		private class EvalfPattern : ITargetPattern
		{
			public delegate bool EvalPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn);

			private readonly EvalPattern EvalPatternDelegate;

			public EvalfPattern(EvalPattern evalPattern)
				=> EvalPatternDelegate = evalPattern ?? throw new ArgumentNullException();

			public bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn)
				=> EvalPatternDelegate(anchorRow, anchorColumn, targetRow, targetColumn);

			public bool IsInPattern(Coordinate anchor, Coordinate target)
				=> IsInPattern(anchor.Row, anchor.Column, target.Row, target.Column);
		}
	}
}
