using SnakeApp.Graphics;
using SnakeApp.Models;

namespace SnakeApp.Factories
{
	public class SnakeFactory
	{
		public static Snake CreateSnake(int initialSize, Point initialHeadPosition)
		{
			var snake = new Snake();
			snake.Direction = Direction.Right;

			snake.SnakePoints.Clear();
			snake.SnakePoints.Add(initialHeadPosition);

			for (int i = 1; i < initialSize; i++)
			{
				var bodyPoint = new Point
				{
					X = snake.SnakePoints[i - 1].X - 1,
					Y = snake.SnakePoints[i - 1].Y
				};
				snake.SnakePoints.Add(bodyPoint);
			}

			return snake;
		}
	}
}
