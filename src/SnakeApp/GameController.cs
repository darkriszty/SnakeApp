using SnakeApp.Models;
using System;
using System.Threading.Tasks;

namespace SnakeApp
{
	public class GameController
	{
		public async Task StartNewGame()
		{
			PrepareConsole();

			var game = new Game(80, 25, 5, 100);
			game.Start();

			ConsoleKeyInfo userInput = new ConsoleKeyInfo();
			do
			{
				userInput = Console.ReadKey(true);
				game.ReceiveInput(userInput.Key);
			} while (userInput.Key != ConsoleKey.Q);
			
			RestoreConsole();
		}

		private void PrepareConsole()
		{
			// getting the current cursor visibility is not supported on linux, so just hide then restore it
			Console.Clear();
			Console.CursorVisible = false;
		}

		private void RestoreConsole()
		{
			Console.Clear();
			Console.CursorVisible = true;
		}
	}
}
