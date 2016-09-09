using SnakeApp.Graphics;
using System.Collections.Generic;

namespace SnakeApp.Models
{
	public class Snake
	{
		public Snake()
		{
			SnakePoints = new List<Point>();
			GrowingPointIndicators = new List<GrowingPointIndicator>();
			DirectionQueue = new Queue<Direction>();
		}

		public Queue<Direction> DirectionQueue { get; private set; }

		public Point LastSnakePointToErase { get; set; }

		public List<Point> SnakePoints { get; private set; }

		public List<GrowingPointIndicator> GrowingPointIndicators { get; private set; }
	}
}
