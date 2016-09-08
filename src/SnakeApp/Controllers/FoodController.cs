using SnakeApp.Configuration;
using SnakeApp.Extensions;
using SnakeApp.Graphics;
using SnakeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeApp.Controllers
{
	public class FoodController
	{
		private readonly Random _random = new Random();
		private readonly List<Food> _food = new List<Food>();
		private readonly Snake _snake;
		private readonly SnakeController _snakeController;
		private readonly AppSettings _appSettings;

		public FoodController(Snake snake, SnakeController snakeController, AppSettings appSettings)
		{
			_snake = snake;
			_snakeController = snakeController;
			_appSettings = appSettings;
		}

		public void RemoveExpiredFood()
		{
			for (int i = _food.Count - 1; i >= 0; i--)
			{
				if (_food[i].CanRemove())
					_food.RemoveAt(i);
			}
		}

		public void SpawnFoodIfRequired()
		{
			if (_food.Count == _appSettings.MaxFoodCount)
				return;
			Point foodPosition = FindFoodSpot();
			Food f = new Food(_appSettings.FoodScore, foodPosition, _appSettings.FoodTTLInSeconds);
			_food.Add(f);
		}

		public byte ConsumedFoodAtCurrentPosition(Snake snake)
		{
			foreach (var food in _food)
			{
				if (food.Position == snake.Head())
				{
					// signal the snake to consume food
					_snakeController.Consume(_snake, food);

					// no other food can be intersecting with the head at the same time, so save a few useless iterations by breaking
					return food.Score;
				}
			}

			return 0;
		}

		public void DrawAllFood()
		{
			foreach (var food in _food)
			{
				Draw(food);
			}
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
				x = _random.Next(1, _appSettings.WorldWidth);
				y = _random.Next(2, _appSettings.WorldHeight);
			}
			return new Point(x, y);
		}

		public static void Draw(Food food)
		{
			// after the food becomes old the world must first draw it (aka remove it)
			// then the CanRemove property returns true such that it is removed from the world and the position is freed up
			if (food.IsExpired())
			{
				PointDrawing.ErasePoint(food.Position);
				food.DrawnAsRotten = true;
			}
			else
			{
				PointDrawing.DrawPoint(food.Position, '*');
			}
		}
	}
}
