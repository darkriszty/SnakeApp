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
		}
		
		public Direction Direction { get; set; }

		public Point LastSnakePointToErase { get; set; }

		public List<Point> SnakePoints { get; private set; }

		public List<GrowingPointIndicator> GrowingPointIndicators { get; private set; }
	}
}
