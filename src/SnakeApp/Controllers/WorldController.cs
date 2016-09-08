using SnakeApp.Extensions;
using SnakeApp.Graphics;
using SnakeApp.Models;
using System;
using System.Collections.Generic;

namespace SnakeApp.Controllers
{
	public class WorldController
	{
		private readonly World _world;
		private readonly SnakeController _snakeController;
		private readonly FoodController _foodController;

		public WorldController(World world, SnakeController snakeController, FoodController foodController)
		{
			_world = world;
			_snakeController = snakeController;
			_foodController = foodController;
		}

		public GameStepResult Advance()
		{
			_foodController.SpawnFoodIfRequired();

			// remove any old food
			_foodController.RemoveExpiredFood();

			// advance the snake
			_snakeController.Advance(_world.Snake);

			// check if the snake head intersects with a food
			byte consumedFoodScore = _foodController.ConsumedFoodAtCurrentPosition(_world.Snake);

			bool snakeDied = DidSnakeReachObstacle();

			return new GameStepResult
			{
				GameOver = snakeDied,
				ConsumedFoodScore = consumedFoodScore
			};
		}

		public void Draw()
		{
			// draw border
			DrawBorders();
			_snakeController.Draw(_world.Snake);
			// drawing the food after the snake creates an 'eating' effect
			_foodController.DrawAllFood();
		}

		public void ReceiveInput(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.RightArrow:
					_world.Snake.SetDirection(Direction.Right);
					break;

				case ConsoleKey.LeftArrow:
					_world.Snake.SetDirection(Direction.Left);
					break;

				case ConsoleKey.UpArrow:
					_world.Snake.SetDirection(Direction.Up);
					break;

				case ConsoleKey.DownArrow:
					_world.Snake.SetDirection(Direction.Down);
					break;
			}
		}

		private bool DidSnakeReachObstacle()
		{
			// get all occupied points that will make the snake die
			List<Point> snakeOccupiedPoints = new List<Point>();

			// the snake is not allowed to collide with itself
			snakeOccupiedPoints.AddRange(_world.Snake.GetOccupiedPoints());
			// exclude head, otherwise it's game over from start
			snakeOccupiedPoints.RemoveAt(0);
			if (snakeOccupiedPoints.Contains(_world.Snake.Head()))
				return true;

			// the snake is not allowed to hit the borders
			if (_world.Snake.Head().X < 1)
				return true;
			if (_world.Snake.Head().X >= _world.Width)
				return true;
			if (_world.Snake.Head().Y < 2)
				return true;
			if (_world.Snake.Head().Y >= _world.Height)
				return true;

			return false;
		}

		private void DrawBorders()
		{
			if (_world.BordersDrawn)
				return;

			var p = new Point();
			// top
			for (int i = 0; i < _world.Width; i++)
			{
				p.X = i;
				p.Y = 1;
				PointDrawing.DrawPoint(p, '=');
			}
			// bottom
			for (int i = 0; i < _world.Width; i++)
			{
				p.X = i;
				p.Y = _world.Height;
				PointDrawing.DrawPoint(p, '=');
			}
			// left
			for (int i = 1; i <= _world.Height; i++)
			{
				p.X = 0;
				p.Y = i;
				PointDrawing.DrawPoint(p, '|');
			}
			// right
			for (int i = 1; i <= _world.Height; i++)
			{
				p.X = _world.Width;
				p.Y = i;
				PointDrawing.DrawPoint(p, '|');
			}
			// top left corner
			p.X = 0;
			p.Y = 1;
			PointDrawing.DrawPoint(p, '#');
			// bottom left corner
			p.X = 0;
			p.Y = _world.Height;
			PointDrawing.DrawPoint(p, '#');

			// top right corner
			p.X = _world.Width;
			p.Y = 1;
			PointDrawing.DrawPoint(p, '#');
			// bottom right corner
			p.X = _world.Width;
			p.Y = _world.Height;
			PointDrawing.DrawPoint(p, '#');
			_world.BordersDrawn = true;
		}
	}
}
