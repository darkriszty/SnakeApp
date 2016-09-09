using SnakeApp.Configuration;
using SnakeApp.Extensions;
using SnakeApp.Graphics;
using SnakeApp.Models;

namespace SnakeApp.Factories
{
	public class SnakeFactory
	{
		private readonly AppSettings _appSettings;

		public SnakeFactory(AppSettings appSettings)
		{
			_appSettings = appSettings;
		}

		public Snake CreateSnake()
		{
			var snake = new Snake();
			snake.SetDirection(Direction.Right);

			var initialHeadPosition = GetInitialSnakeHeadPosition(_appSettings.WorldWidth, _appSettings.WorldHeight, _appSettings.InitialSnakeSize);
			snake.SnakePoints.Add(initialHeadPosition);

			for (int i = 1; i < _appSettings.InitialSnakeSize; i++)
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

		private Point GetInitialSnakeHeadPosition(byte worldWidth, byte worldHeight, byte initialSnakeSize)
		{
			Point p = new Point();
			p.Y = worldHeight / 2;
			p.X = (worldWidth / 2) - (initialSnakeSize / 2);
			return p;
		}
	}
}
