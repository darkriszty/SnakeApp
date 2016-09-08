using SnakeApp.Factories;
using SnakeApp.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeApp.Controllers
{
	public class GameController
	{
		private Game _game;
		private CancellationTokenSource _gameCancellationTokenSource;
		private readonly GameFactory _gameFactory;
		private readonly World _world;
		private readonly WorldController _worldController;

		public GameController(GameFactory gameFactory, World world, WorldController worldController)
		{
			_gameFactory = gameFactory;
			_world = world;
			_worldController = worldController;
		}

		public void StartNewGame()
		{
			PrepareConsole();

			_game = _gameFactory.CreateNewGame(_world);
			StartAsync();

			ConsoleKeyInfo userInput = new ConsoleKeyInfo();
			do
			{
				userInput = Console.ReadKey(true);
				ReceiveInput(userInput.Key);
			} while (userInput.Key != ConsoleKey.Q);

			RestoreConsole();
		}


		private Task StartAsync()
		{
			_game.IsOver = false;
			// create cancellation token, run main loop, return the task
			_gameCancellationTokenSource = new CancellationTokenSource();
			return MainLoop(_gameCancellationTokenSource.Token);
		}

		public void Stop()
		{
			_game.IsPaused = true;
			_gameCancellationTokenSource.Cancel();
		}

		public void ReceiveInput(ConsoleKey key)
		{
			if (key == ConsoleKey.Q)
			{
				Stop();
				return;
			}
			if (key == ConsoleKey.P || key == ConsoleKey.Pause)
			{
				if (_game.IsPaused)
					StartAsync();
				else
					Stop();
			}

			// send the input to the world, the world decides what game objects it should affect
			_worldController.ReceiveInput(key);
		}

		private async Task MainLoop(CancellationToken cancellationToken)
		{
			while (true)
			{
				if (cancellationToken.IsCancellationRequested)
					break;

				var gameStepResult = _worldController.Advance();

				if (gameStepResult.GameOver)
				{
					GameOver();
					break;
				}

				_game.Score.FoodConsumed += gameStepResult.ConsumedFoodScore;

				_worldController.Draw();
				ShowScore();

				await Task.Delay(_game.Speed);
			}
		}

		private void GameOver()
		{
			Stop();
			Console.Title = "Game over";
			_game.IsOver = true;
		}

		private void ShowScore()
		{
			Console.SetCursorPosition(0, 0);
			Console.Write(_game.Score.FoodConsumed);
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
