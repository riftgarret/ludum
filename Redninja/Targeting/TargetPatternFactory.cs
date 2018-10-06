namespace Redninja.Targeting
{
	/// <summary>
	/// Helper method for building out patterns.
	/// </summary>
	public class TargetPatternFactory
    {
        public static ITargetPattern CreateRowPattern() => new EvalfPattern((ar, ac, tr, tc) => tr == ar);

        public static ITargetPattern CreateColumnPattern() => new EvalfPattern((ar, ac, tr, tc) => tc == ac);


        /// <summary>
        /// Create simple delegate to handle what the interface does
        /// </summary>
        internal class EvalfPattern : ITargetPattern
        {
            internal delegate bool EvalPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn);

            private EvalPattern EvalPatternDelegate;

            internal EvalfPattern(EvalPattern evalPattern)
            {
                EvalPatternDelegate = evalPattern;
            }

            public bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn)
            {
                return EvalPatternDelegate.Invoke(anchorRow, anchorColumn, targetRow, targetColumn);
            }
        }
    }
}
