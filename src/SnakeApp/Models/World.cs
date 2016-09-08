using SnakeApp.Controllers;
using SnakeApp.Drawing;
using System;
using System.Collections.Generic;

namespace SnakeApp.Models
{
	public class World
	{
		private bool _bordersDrawn;
		private readonly Snake _snake;
		private readonly byte _width;
		private readonly byte _height;
		private FoodController _foodHandler;
		private readonly Action _snakeDied;

		public World(Snake snake, byte width, byte height, FoodController foodHandler, Action snakeDied)
		{
			_snake = snake;
			_width = width;
			_height = height;
			_foodHandler = foodHandler;
			_snakeDied = snakeDied;
		}

		public void Advance()
		{
			_foodHandler.SpawnFoodIfRequired();

			// remove any old food
			_foodHandler.RemoveExpiredFood();

			// advance the snake
			_snake.Advance();

			// check if the snake head intersects with a food
			_foodHandler.SnakeIntersectsWithFood(_snake);

			bool snakeDied = DidSnakeReachObstacle();
			if (snakeDied)
				_snakeDied();
		}

		public void Draw()
		{
			// draw border
			DrawBorders();
			_snake.Draw();
			// drawing the food after the snake creates an 'eating' effect
			_foodHandler.DrawAllFood();
		}

		public void ReceiveInput(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.RightArrow:
					_snake.SetDirection(Direction.Right);
					break;

				case ConsoleKey.LeftArrow:
					_snake.SetDirection(Direction.Left);
					break;

				case ConsoleKey.UpArrow:
					_snake.SetDirection(Direction.Up);
					break;

				case ConsoleKey.DownArrow:
					_snake.SetDirection(Direction.Down);
					break;
			}
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

		private void DrawBorders()
		{
			if (_bordersDrawn)
				return;

			var p = new Point();
			// top
			for (int i = 0; i < _width; i++)
			{
				p.X = i;
				p.Y = 1;
				PointDrawing.DrawPoint(p, '=');
			}
			// bottom
			for (int i = 0; i < _width; i++)
			{
				p.X = i;
				p.Y = _height;
				PointDrawing.DrawPoint(p, '=');
			}
			// left
			for (int i = 1; i <= _height; i++)
			{
				p.X = 0;
				p.Y = i;
				PointDrawing.DrawPoint(p, '|');
			}
			// right
			for (int i = 1; i <= _height; i++)
			{
				p.X = _width;
				p.Y = i;
				PointDrawing.DrawPoint(p, '|');
			}
			// top left corner
			p.X = 0;
			p.Y = 1;
			PointDrawing.DrawPoint(p, '#');
			// bottom left corner
			p.X = 0;
			p.Y = _height;
			PointDrawing.DrawPoint(p, '#');

			// top right corner
			p.X = _width;
			p.Y = 1;
			PointDrawing.DrawPoint(p, '#');
			// bottom right corner
			p.X = _width;
			p.Y = _height;
			PointDrawing.DrawPoint(p, '#');
			_bordersDrawn = true;
		}
	}
}
