﻿using SnakeApp.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace SnakeApp.Models
{
	public class Snake
	{
		private const char HEAD_CHAR = '@';
		private const char BODY_CHAR = '.';
		private readonly List<Point> _snakePoints;
		private Direction _direction;
		private Point _lastSnakePointToErase;
		private readonly List<GrowingPointIndicator> _growingPointIndicators;

		public Snake(int initialSize, Point initialHeadPosition, Direction initialDirection = Direction.Right)
		{
			_snakePoints = new List<Point>();
			_growingPointIndicators = new List<GrowingPointIndicator>();
			_direction = initialDirection;
			InitSnake(initialHeadPosition, initialSize, initialDirection);
		}

		public Point Head
		{
			get { return _snakePoints.Any()	? new Point(_snakePoints[0]) : null; }
		}

		public void Advance()
		{
			Point nextHead = GetNextHead();

			// add the new head
			_snakePoints.Insert(0, nextHead);

			// delete the last snake point
			_lastSnakePointToErase = _snakePoints[_snakePoints.Count - 1];
			_snakePoints.RemoveAt(_snakePoints.Count - 1);

			// check if the snake has to grow
			for(int i = 0; i < _growingPointIndicators.Count; i++)
			{
				var p = _growingPointIndicators[i];

				// do the actual growing at a predefined position (which means a predefined moment in time)
				if (Head == p.TailPositionWhenToGrow)
				{
					_snakePoints.Add(p.OriginalFoodPosition);
					_growingPointIndicators.RemoveAt(i);
					i--;
				}
			}
		}

		public void Consume(Food food)
		{
			// mark the food as eaten
			food.IsConsumed = true;

			// Save the next position the snake head will land in. That will indicate when snake's tail will grow
			var growingPointIndicator = new GrowingPointIndicator();
			growingPointIndicator.OriginalFoodPosition = new Point(food.Position);
			
			Point nextHead = GetNextHead();
			growingPointIndicator.TailPositionWhenToGrow = nextHead;

			// when the tail of the snake leaves the position of the food then the snake grows by adding one more point to its tail
			_growingPointIndicators.Add(growingPointIndicator);
		}

		public void SetDirection(Direction requestedDirection)
		{
			if (!_direction.IsOpposite(requestedDirection))
				_direction = requestedDirection;
		}

		public void Draw()
		{
			PointDrawing.DrawPoint(_snakePoints[0], HEAD_CHAR);
			for (int i = 1; i < _snakePoints.Count; i++)
			{
				PointDrawing.DrawPoint(_snakePoints[i], BODY_CHAR);
			}

			if (_lastSnakePointToErase == null)
				return;

			PointDrawing.ErasePoint(_lastSnakePointToErase);
		}

		public List<Point> GetOccupiedPoints()
		{
			var occupiedPoints = new List<Point>();
			occupiedPoints.AddRange(_snakePoints.Select(s => new Point(s)));
			return occupiedPoints;
		}

		private void InitSnake(Point initialHeadPosition, int initialSize, Direction initialDirection)
		{
			_snakePoints.Clear();
			_snakePoints.Add(initialHeadPosition);
			// consider implementing this for other directions, so for instance the initial direction could be random
			//if (initialDirection == Direction.Right)
			{
				for (int i = 1; i < initialSize; i++)
				{
					var bodyPoint = new Point
					{
						X = _snakePoints[i - 1].X - 1,
						Y = _snakePoints[i - 1].Y
					};
					_snakePoints.Add(bodyPoint);
				}
			}
		}

		private Point GetNextHead()
		{
			Point nextHead = null;
			switch (_direction)
			{
				case Direction.Left:
					nextHead = new Point { X = _snakePoints[0].X - 1, Y = _snakePoints[0].Y };
					break;

				case Direction.Right:
					nextHead = new Point { X = _snakePoints[0].X + 1, Y = _snakePoints[0].Y };
					break;

				case Direction.Up:
					nextHead = new Point { X = _snakePoints[0].X, Y = _snakePoints[0].Y - 1 };
					break;

				case Direction.Down:
					nextHead = new Point { X = _snakePoints[0].X, Y = _snakePoints[0].Y + 1 };
					break;

				default:
					break;
			}

			return nextHead;
		}

		private class GrowingPointIndicator
		{
			public Point OriginalFoodPosition { get; set; }
			public Point TailPositionWhenToGrow { get;set;}
		}
	}
}
