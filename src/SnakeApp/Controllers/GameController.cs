using SnakeApp.Models;
using System;

namespace SnakeApp.Controllers
{
	public class GameController
	{
		private byte MAX_WIDTH = 80;
		private byte MAX_HEIGHT = 25;

		public void StartNewGame()
		{
			PrepareConsole();

			var game = new Game(Math.Min((byte)(Console.WindowWidth - 1), MAX_WIDTH), Math.Min((byte)(Console.WindowHeight - 1), MAX_HEIGHT), 5, 100);
			game.StartAsync();

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
