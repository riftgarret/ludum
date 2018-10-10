namespace Redninja.Targeting
{
	public class SelectedTargetPattern
	{
		public ITargetingRule Rule { get; }
		public Coordinate Anchor { get; }

		public SelectedTargetPattern(ITargetingRule rule, int anchorRow, int anchorColumn)
			: this(rule, new Coordinate(anchorRow, anchorColumn))
		{ }

		public SelectedTargetPattern(ITargetingRule rule, Coordinate anchor)
		{
			Rule = rule;
			Anchor = anchor;
		}
	}
}
