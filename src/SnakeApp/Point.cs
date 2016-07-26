namespace SnakeApp
{
	public class Point
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Point() { }
		public Point(int x, int y) : this()
		{
			X = x;
			Y = y;
		}
		public Point(Point other) : this()
		{
			X = other.X;
			Y = other.Y;
		}

		public static bool operator == (Point a, Point b)
		{
			if (ReferenceEquals(a, b))
				return true;

			// if one is null then not equal (cast to object to avoid infinite loop of the '==' operator)
			if (((object)a == null) || ((object)b == null))
				return false;

			// if their components match then they're equal
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator != (Point a, Point b)
		{
			return !(a == b);
		}
	}
}
