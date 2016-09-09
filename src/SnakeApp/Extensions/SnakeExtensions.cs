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

		public static Direction GetDirection(this Snake snake)
		{
			if (snake.DirectionQueue.Count == 0)
				return Direction.NotSet;
			return snake.DirectionQueue.Peek();
		}

		public static void SetDirection(this Snake snake, Direction requestedDirection)
		{
			// two consecutive moves can't be opposite
			if (!snake.GetDirection().IsOpposite(requestedDirection))
				snake.DirectionQueue.Enqueue(requestedDirection);
		}

		public static List<Point> GetSnakePoints(this Snake snake)
		{
			var occupiedPoints = new List<Point>();
			occupiedPoints.AddRange(snake.SnakePoints.Select(s => new Point(s)));
			return occupiedPoints;
		}
	}
}
