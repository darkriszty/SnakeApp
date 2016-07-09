using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeApp.Models
{
	public class World : IGameObject
	{
		private readonly IGameObject _snake;
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
	}
}
