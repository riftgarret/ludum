namespace Redninja
{
	public struct Coordinate
	{
		public int Row { get; }
		public int Column { get; }

		public Coordinate(int row, int column)
		{
			Row = row;
			Column = column;
		}

		//public override bool Equals(object obj)
		//{
		//	if (obj is Coordinate c)
		//	{
		//		return c.Row == Row && c.Column == Column;
		//	}
		//	else return base.Equals(obj);
		//}
	}
}
