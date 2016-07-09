using System;
using System.Collections.Generic;
using System.Linq;
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
		private readonly IGameObject _world;
		private readonly int _speed;
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
			// create cancellation token, run main loop, return the task
			_gameCancellationTokenSource = new CancellationTokenSource();
			return MainLoop(_gameCancellationTokenSource.Token);
		}

		public async Task Stop()
		{
			_gameCancellationTokenSource.Cancel();
		}

		private async Task MainLoop(CancellationToken cancellationToken)
		{
			while (true)
			{
				if (cancellationToken.IsCancellationRequested)
					break;

				await _world.Advance();
				await _world.Draw();

				await Task.Delay(_speed);
			}
		}
	}
}
