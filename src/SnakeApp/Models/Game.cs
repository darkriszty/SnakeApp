﻿using SnakeApp.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeApp.Models
{
	/// <summary>
	/// It's an abstract game object, having a "real" world.
	/// Sends the input to the world, the world decides what game objects it should affect
	/// </summary>
	public class Game
	{
		private readonly World _world;
		private readonly Score _score;
		private readonly int _speed;
		private bool _isPaused;
		private CancellationTokenSource _gameCancellationTokenSource;

		public bool IsOver { get; private set; }

		public Game(byte worldWidth, byte worldHeight, byte initialSnakeSize, int gameSpeed)
		{
			var initialSnakeHeadPosition = GetInitialSnakeHeadPosition(worldWidth, worldHeight, initialSnakeSize);
			var snake = new Snake(initialSnakeSize, initialSnakeHeadPosition);
			var foodHandler = new FoodController(worldWidth, worldHeight, snake, FoodEatenCallback);
			_world = new World(snake, worldWidth, worldHeight, foodHandler, SnakeDiedCallback);
			_score = new Score();
			_speed = gameSpeed;
		}

		public Task StartAsync()
		{
			_isPaused = false;
			// create cancellation token, run main loop, return the task
			_gameCancellationTokenSource = new CancellationTokenSource();
			return MainLoop(_gameCancellationTokenSource.Token);
		}

		public void Stop()
		{
			_isPaused = true;
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
				if (_isPaused)
					StartAsync();
				else
					Stop();
			}
			
			_world.ReceiveInput(key);
		}

		private async Task MainLoop(CancellationToken cancellationToken)
		{
			while (true)
			{
				if (cancellationToken.IsCancellationRequested)
					break;

				_world.Advance();
				_world.Draw();
				ShowScore();

				await Task.Delay(_speed);
			}
		}

		private Point GetInitialSnakeHeadPosition(byte worldWidth, byte worldHeight, byte initialSnakeSize)
		{
			Point p = new Point();
			p.Y = worldHeight / 2;
			p.X = (worldWidth / 2) - (initialSnakeSize / 2);
			return p;
		}

		private void FoodEatenCallback(byte foodScore)
		{
			_score.FoodConsumed += foodScore;
		}

		private void SnakeDiedCallback()
		{
			Stop();
			// TODO: consider moving this logic with the main loop into the game controller 
			Console.Title = "Game over";
			IsOver = true;
		}

		private void ShowScore()
		{
			Console.SetCursorPosition(0, 0);
			Console.Write(_score.FoodConsumed);
		}
	}
}
