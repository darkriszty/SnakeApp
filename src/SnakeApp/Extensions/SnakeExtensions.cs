using SnakeApp.Graphics;
using SnakeApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SnakeApp.Extensions
{
	public static class SnakeExtensions
	{
		public static Point Head(this Snake snake)
		{
			return snake.SnakePoints.Any() ? new Point(snake.SnakePoints[0]) : null;
		}

		public static void SetDirection(this Snake snake, Direction requestedDirection)
		{
			if (!snake.Direction.IsOpposite(requestedDirection))
				snake.Direction = requestedDirection;
		}

		public static List<Point> GetOccupiedPoints(this Snake snake)
		{
			var occupiedPoints = new List<Point>();
			occupiedPoints.AddRange(snake.SnakePoints.Select(s => new Point(s)));
			return occupiedPoints;
		}

		public static Point GetNextHead(this Snake snake)
		{
			Point nextHead = null;
			switch (snake.Direction)
			{
				case Direction.Left:
					nextHead = new Point { X = snake.SnakePoints[0].X - 1, Y = snake.SnakePoints[0].Y };
					break;

				case Direction.Right:
					nextHead = new Point { X = snake.SnakePoints[0].X + 1, Y = snake.SnakePoints[0].Y };
					break;

				case Direction.Up:
					nextHead = new Point { X = snake.SnakePoints[0].X, Y = snake.SnakePoints[0].Y - 1 };
					break;

				case Direction.Down:
					nextHead = new Point { X = snake.SnakePoints[0].X, Y = snake.SnakePoints[0].Y + 1 };
					break;

				default:
					break;
			}

			return nextHead;
		}
	}
}
