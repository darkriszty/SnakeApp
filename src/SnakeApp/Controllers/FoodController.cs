using SnakeApp.Drawing;
using SnakeApp.Extensions;
using SnakeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeApp.Controllers
{
	public class FoodController
	{
		private const int MAX_FOOD_COUNT = 1;
		private const byte FOOD_SCORE = 1;
		private const byte FOOD_TTL = 10;
		private readonly Random _random = new Random();
		private readonly List<Food> _food = new List<Food>();
		private readonly byte _worldWidth;
		private readonly byte _worldHeight;
		private readonly Snake _snake;
		private readonly Action<byte> _foodEaten;

		public FoodController(byte worldWidth, byte worldHeight, Snake snake, Action<byte> foodEaten)
		{
			_worldWidth = worldWidth;
			_worldHeight = worldHeight;
			_snake = snake;
			_foodEaten = foodEaten;
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
			if (_food.Count == MAX_FOOD_COUNT)
				return;
			Point foodPosition = FindFoodSpot();
			Food f = new Food(FOOD_SCORE, foodPosition, FOOD_TTL);
			_food.Add(f);
		}

		public void SnakeIntersectsWithFood(Snake snake)
		{
			foreach (var food in _food)
			{
				if (food.Position == snake.Head)
				{
					// signal the snake to consume food
					snake.Consume(food);

					// signal the world that the snake has grown
					_foodEaten(food.Score);

					// no other food can be intersecting with the head at the same time, so save a few useless iterations by breaking
					break;
				}
			}
		}

		public void DrawAllFood()
		{
			foreach (var food in _food)
			{
				food.Draw();
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
				x = _random.Next(1, _worldWidth);
				y = _random.Next(2, _worldHeight);
			}
			return new Point(x, y);
		}
	}
}
