using System;
using System.Threading.Tasks;

namespace SnakeApp.Models
{
	public class World
	{
		private readonly Snake _snake;
		private readonly byte _width;
		private readonly byte _height;

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
	}
}
