using SnakeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeApp
{
	public class GameController
	{
		public async Task StartNewGame()
		{
			var game = new Game(80, 40, 5, 100);
			await game.Start();
		}
	}
}
