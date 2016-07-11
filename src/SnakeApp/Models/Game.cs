﻿using System;
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
		private readonly int _speed;
		private bool _isPaused;
		private CancellationTokenSource _gameCancellationTokenSource;

		public bool IsOver { get; private set; }

		public Game(byte worldWidth, byte worldHeight, byte initialSnakeSize, int gameSpeed)
		{
			Point initialSnakeHeadPosition = GetInitialSnakeHeadPosition(worldWidth, worldHeight, initialSnakeSize);
			_world = new World(new Snake(initialSnakeSize, initialSnakeHeadPosition), worldWidth, worldHeight);
			_speed = gameSpeed;
		}

		public Point GetInitialSnakeHeadPosition(byte worldWidth, byte worldHeight, byte initialSnakeSize)
		{
			Point p = new Point();
			p.Y = worldHeight / 2;
			p.X = (worldWidth / 2) - (initialSnakeSize / 2);
			return p;
		}

		public Task Start()
		{
			_isPaused = false;
			// create cancellation token, run main loop, return the task
			_gameCancellationTokenSource = new CancellationTokenSource();
			return MainLoop(_gameCancellationTokenSource.Token);
		}

		public async Task Stop()
		{
			_isPaused = true;
			_gameCancellationTokenSource.Cancel();
		}

		public async Task ReceiveInput(ConsoleKey key)
		{
			if (key == ConsoleKey.Q)
			{
				await Stop();
				return;
			}
			if (key == ConsoleKey.P || key == ConsoleKey.Pause)
			{
				if (_isPaused)
					await Start();
				else
					await Stop();
			}
			
			await _world.ReceiveInput(key);
		}

		private async Task MainLoop(CancellationToken cancellationToken)
		{
			while (true)
			{
				if (cancellationToken.IsCancellationRequested)
					break;

				await _world.Advance();
				await _world.Draw();

				_world.SpawnFood(1, 10);

				await Task.Delay(_speed);
			}
		}
	}
}
