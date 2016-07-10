using System;
using System.Threading.Tasks;

namespace SnakeApp.Models
{
	public class World
	{
		private readonly Snake _snake;
		private readonly byte _width;
		private readonly byte _height;
		private bool _bordersDrawn;

		public World(Snake snake, byte width, byte height)
		{
			_snake = snake;
			_width = width;
			_height = height;
		}

		public async Task Advance()
		{
			await _snake.Advance();
		}

		public async Task Draw()
		{
			// draw border
			await DrawBorders();
			// draw other elements form the world
			await _snake.Draw();
		}

		public async Task ReceiveInput(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.RightArrow:
					await _snake.SetDirection(Direction.Right);
					break;

				case ConsoleKey.LeftArrow:
					await _snake.SetDirection(Direction.Left);
					break;

				case ConsoleKey.UpArrow:
					await _snake.SetDirection(Direction.Up);
					break;

				case ConsoleKey.DownArrow:
					await _snake.SetDirection(Direction.Down);
					break;
			}
		}

		private async Task DrawBorders()
		{
			if (_bordersDrawn)
				return;

			var p = new Point();
			// top
			for (int i = 0; i < _width; i++)
			{
				p.X = i;
				p.Y = 1;
				await PointDrawing.DrawPoint(p, '=');
			}
			// bottom
			for (int i = 0; i < _width; i++)
			{
				p.X = i;
				p.Y = _height;
				await PointDrawing.DrawPoint(p, '=');
			}
			// left
			for (int i = 1; i < _height; i++)
			{
				p.X = 0;
				p.Y = i;
				await PointDrawing.DrawPoint(p, '|');
			}
			// right
			for (int i = 1; i < _height; i++)
			{
				p.X = _width;
				p.Y = i;
				await PointDrawing.DrawPoint(p, '|');
			}
			// top left corner
			p.X = 0;
			p.Y = 1;
			await PointDrawing.DrawPoint(p, '#');
			// bottom left corner
			p.X = 0;
			p.Y = _height;
			await PointDrawing.DrawPoint(p, '#');

			// top right corner
			p.X = _width;
			p.Y = 1;
			await PointDrawing.DrawPoint(p, '#');
			// bottom right corner
			p.X = _width;
			p.Y = _height;
			await PointDrawing.DrawPoint(p, '#');
			_bordersDrawn = true;
		}
	}
}
