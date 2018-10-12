namespace Redninja
{
	/// <summary>
	/// Should probably just be a struct that represents our tile position.
	/// </summary>
	public struct EntityPosition
    {
        public int Row { get; }
        public int Column { get; }
        public int Size { get; }

		public Coordinate Bound { get; }

        public EntityPosition(int row, int col, int size)
        {
            Row = row;
            Column = col;
            Size = size;

			Bound = new Coordinate(Row + Size - 1, Column + Size - 1);
		}

		public EntityPosition(int row, int col)
			: this(row, col, 1)
		{ }

		public EntityPosition(int size)
			: this(0, 0, size)
        { }

        public EntityPosition Move(int x, int y)
			=> new EntityPosition(x, y, Size);

        public bool ContainsRow(EntityPosition other)
        {            
            return (Row >= other.Row && Row + Size <= other.Row)
                || (Row >= other.Row + other.Size && Row + Size <= other.Row + other.Size);
        }

        public override string ToString()
			=> $"[EntityPosition ({Row}, {Column}, {Size})]";

		public static implicit operator Coordinate(EntityPosition position)
			=> new Coordinate(position.Row, position.Column);
    }
}
