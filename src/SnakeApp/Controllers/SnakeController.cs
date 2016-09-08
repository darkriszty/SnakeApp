using SnakeApp.Extensions;
using SnakeApp.Graphics;
using SnakeApp.Models;

namespace SnakeApp.Controllers
{
	public class SnakeController
	{
		private const char HEAD_CHAR = '@';
		private const char BODY_CHAR = '.';

		public void Advance(Snake snake)
		{
			Point nextHead = snake.GetNextHead();

			// add the new head
			snake.SnakePoints.Insert(0, nextHead);

			// delete the last snake point
			snake.LastSnakePointToErase = snake.SnakePoints[snake.SnakePoints.Count - 1];
			snake.SnakePoints.RemoveAt(snake.SnakePoints.Count - 1);

			// check if the snake has to grow
			for (int i = 0; i < snake.GrowingPointIndicators.Count; i++)
			{
				var p = snake.GrowingPointIndicators[i];

				// do the actual growing at a predefined position (which means a predefined moment in time)
				if (snake.Head() == p.TailPositionWhenToGrow)
				{
					snake.SnakePoints.Add(p.OriginalFoodPosition);
					snake.GrowingPointIndicators.RemoveAt(i);
					i--;
				}
			}
		}

		public void Consume(Snake snake, Food food)
		{
			// mark the food as eaten
			food.IsConsumed = true;

			// Save the next position the snake head will land in. That will indicate when snake's tail will grow
			var growingPointIndicator = new GrowingPointIndicator();
			growingPointIndicator.OriginalFoodPosition = new Point(food.Position);

			Point nextHead = snake.GetNextHead();
			growingPointIndicator.TailPositionWhenToGrow = nextHead;

			// when the tail of the snake leaves the position of the food then the snake grows by adding one more point to its tail
			snake.GrowingPointIndicators.Add(growingPointIndicator);
		}

		public void Draw(Snake snake)
		{
			PointDrawing.DrawPoint(snake.SnakePoints[0], HEAD_CHAR);
			for (int i = 1; i < snake.SnakePoints.Count; i++)
			{
				PointDrawing.DrawPoint(snake.SnakePoints[i], BODY_CHAR);
			}

			if (snake.LastSnakePointToErase == null)
				return;

			PointDrawing.ErasePoint(snake.LastSnakePointToErase);
		}
	}
}
