namespace SnakeApp.Models
{
	public enum Direction
	{
		Up,
		Down,
		Left,
		Right
	}

	public static class DirectionExtensions
	{
		public static bool IsOpposite(this Direction currentDirection, Direction requestedDirection)
		{
			switch (requestedDirection)
			{
				case Direction.Left:
					return currentDirection == Direction.Right;
				case Direction.Right:
					return currentDirection == Direction.Left;
				case Direction.Up:
					return currentDirection == Direction.Down;
				case Direction.Down:
					return currentDirection == Direction.Up;
				default:
					return false;
			}
		}
	}
}
