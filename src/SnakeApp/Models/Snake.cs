using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnakeApp.Models
{
	public class Snake
	{
		private const char HEAD_CHAR = '@';
		private const char BODY_CHAR = '.';
		private readonly List<Point> _snakePoints;
		private Direction _direction;
		private Point _lastSnakePointToErase;

		public Snake(int initialSize, Point initialHeadPosition, Direction initialDirection = Direction.Right)
		{
			_snakePoints = new List<Point>();
			_direction = initialDirection;
			InitSnake(initialHeadPosition, initialSize, initialDirection);
		}

		public async Task Advance()
		{
			Point newHead = null;
			switch (_direction)
			{
				case Direction.Left:
					newHead = new Point { X = _snakePoints[0].X - 1, Y = _snakePoints[0].Y };
					break;

				case Direction.Right:
					newHead = new Point { X = _snakePoints[0].X + 1, Y = _snakePoints[0].Y };
					break;

				case Direction.Up:
					newHead = new Point { X = _snakePoints[0].X, Y = _snakePoints[0].Y - 1 };
					break;

				case Direction.Down:
					newHead = new Point { X = _snakePoints[0].X, Y = _snakePoints[0].Y + 1 };
					break;

				default:
					break;
			}

			// add the new head
			_snakePoints.Insert(0, newHead);

			// delete the last snake point
			_lastSnakePointToErase = _snakePoints[_snakePoints.Count - 1];
			_snakePoints.RemoveAt(_snakePoints.Count - 1);
		}

		public async Task SetDirection(Direction requestedDirection)
		{
			if (!_direction.IsOpposite(requestedDirection))
				_direction = requestedDirection;
		}

		public async Task Draw()
		{
			await PointDrawing.DrawPoint(_snakePoints[0], HEAD_CHAR);
			for (int i = 1; i < _snakePoints.Count; i++)
			{
				await PointDrawing.DrawPoint(_snakePoints[i], BODY_CHAR);
			}

			if (_lastSnakePointToErase == null)
				return;

			await PointDrawing.ErasePoint(_lastSnakePointToErase);
		}

		private void InitSnake(Point initialHeadPosition, int initialSize, Direction initialDirection)
		{
			_snakePoints.Clear();
			_snakePoints.Add(initialHeadPosition);
			// TODO: implement for other directions
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
	}
}
