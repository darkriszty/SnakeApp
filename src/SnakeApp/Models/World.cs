using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SnakeApp.Models
{
	public class World
	{
		private readonly List<Food> _food = new List<Food>();
		private readonly Random _random = new Random();
		private readonly Snake _snake;
		private readonly byte _width;
		private readonly byte _height;
		private readonly Action<byte> _foodEaten;
		private readonly Action _snakeDied;
		private bool _bordersDrawn;

		public World(Snake snake, byte width, byte height, Action<byte> foodEaten, Action snakeDied)
		{
			_snake = snake;
			_width = width;
			_height = height;
			_foodEaten = foodEaten;
			_snakeDied = snakeDied;
		}

		public async Task Advance()
		{
			// remove any old food
			for (int i = _food.Count - 1; i >= 0; i--)
			{
				if (_food[i].CanRemove)
					_food.RemoveAt(i);
			}

			// advance the snake
			await _snake.Advance();

			// check if the snake head intersects with a food
			foreach (var food in _food)
			{
				if (food.Position == _snake.Head)
				{
					Console.Title = $"Snake head intersected with food ({food.Position.X},{food.Position.Y})";

					// signal the snake to consume food
					_snake.Consume(food);

					// signal the world that the snake has grown
					_foodEaten(food.Score);

					// no other food can be intersecting with the head at the same time, so save a few useless iterations by breaking
					break;
				}
			}

			bool snakeDied = DidSnakeReachObstacle();
			if (snakeDied)
				_snakeDied();
		}

		public async Task Draw()
		{
			// draw border
			await DrawBorders();
			await _snake.Draw();
			// drawing the food after the snake creates an 'eating' effect
			foreach (var food in _food)
			{
				await food.Draw();
			}
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

		public void SpawnFood(byte foodScore, byte secondsToLive)
		{
			Point foodPosition = FindFoodSpot();
			Food f = new Food(foodScore, foodPosition, secondsToLive);
			_food.Add(f);
		}

		private Point FindFoodSpot()
		{
			// get all the occupied points
			List<Point> allOccupiedPoints = new List<Point>();
			allOccupiedPoints.AddRange(_snake.GetOccupiedPoints());
			allOccupiedPoints.AddRange(_food.Select(f => f.Position));

			// while the x and y are occupied keep generating a new coordinate
			int x = allOccupiedPoints[0].X;
			int y = allOccupiedPoints[0].Y;
			while (allOccupiedPoints.Any(p => p.X == x && p.Y == y))
			{
				x = _random.Next(1, _width);
				y = _random.Next(2, _height);
			}
			return new Point(x, y);
		}

		private bool DidSnakeReachObstacle()
		{
			// get all occupied points that will make the snake die
			List<Point> snakeOccupiedPoints = new List<Point>();

			// the snake is not allowed to collide with itself
			snakeOccupiedPoints.AddRange(_snake.GetOccupiedPoints());
			// exclude head, otherwise it's game over from start
			snakeOccupiedPoints.RemoveAt(0);
			if (snakeOccupiedPoints.Contains(_snake.Head))
				return true;

			// the snake is not allowed to hit the borders
			if (_snake.Head.X < 1)
				return true;
			if (_snake.Head.X >= _width)
				return true;
			if (_snake.Head.Y < 2)
				return true;
			if (_snake.Head.Y >= _height)
				return true;

			return false;
		}

		private async Task DrawBorders()
		{
			if (_bordersDrawn)
				return;

			var p = new Point();
			// top
			for (int i = 0; i < _width; i++)
			{
				p.X = i;
				p.Y = 1;
				await PointDrawing.DrawPoint(p, '=');
			}
			// bottom
			for (int i = 0; i < _width; i++)
			{
				p.X = i;
				p.Y = _height;
				await PointDrawing.DrawPoint(p, '=');
			}
			// left
			for (int i = 1; i <= _height; i++)
			{
				p.X = 0;
				p.Y = i;
				await PointDrawing.DrawPoint(p, '|');
			}
			// right
			for (int i = 1; i <= _height; i++)
			{
				p.X = _width;
				p.Y = i;
				await PointDrawing.DrawPoint(p, '|');
			}
			// top left corner
			p.X = 0;
			p.Y = 1;
			await PointDrawing.DrawPoint(p, '#');
			// bottom left corner
			p.X = 0;
			p.Y = _height;
			await PointDrawing.DrawPoint(p, '#');

			// top right corner
			p.X = _width;
			p.Y = 1;
			await PointDrawing.DrawPoint(p, '#');
			// bottom right corner
			p.X = _width;
			p.Y = _height;
			await PointDrawing.DrawPoint(p, '#');
			_bordersDrawn = true;
		}
	}
}
