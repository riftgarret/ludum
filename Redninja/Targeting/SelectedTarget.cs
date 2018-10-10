using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Targeting
{
	/// <summary>
	/// Selected Target should represent the required meta data for the TargetType.
	/// </summary>
	public class SelectedTarget
	{
		public IBattleEntity TargetEntity { get; }

		public int AnchoredPositionRow { get; }

		public int AnchoredPositionColumn { get; }

		public int Team { get; }

		public SelectedTarget(IBattleEntity target)
			: this(target, target.Position.Row, target.Position.Column, target.Team)
		{

		}

		public SelectedTarget(int anchoredPositionRow, int anchoredPositionColumn, int teamSide)
			: this(null, anchoredPositionRow, anchoredPositionColumn, teamSide)
		{
		}

		private SelectedTarget(
		IBattleEntity targetEntity,
		int anchoredPositionRow,
		int anchoredPositionColumn,
		int teamSide)
		{
			TargetEntity = targetEntity;
			AnchoredPositionRow = anchoredPositionRow;
			AnchoredPositionColumn = anchoredPositionColumn;
			Team = teamSide;
		}
	}
}
