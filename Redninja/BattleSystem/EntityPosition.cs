namespace Redninja.BattleSystem
{
	/// <summary>
	/// Should probably just be a struct that represents our tile position.
	/// </summary>
	public struct EntityPosition
    {
        public int Column { get;  }
        public int Row { get; }
        public int Size { get; }

        public EntityPosition(int row, int col, int size)
        {
            Column = col;
            Row = row;
            Size = size;
        }
        
        public EntityPosition(int size)
        {
            Column = 0;
            Row = 0;
            Size = size;            
        }

        public EntityPosition Move(int x, int y)
        {
            return new EntityPosition(x, y, Size);
        }

        public bool ContainsRow(EntityPosition other)
        {            
            return (Row >= other.Row && Row + Size <= other.Row)
                || (Row >= other.Row + other.Size && Row + Size <= other.Row + other.Size);
        }

        public override string ToString()
        {
            return $"[EntityPosition ({Row}, {Column}, {Size})]";
        }
    }
}
