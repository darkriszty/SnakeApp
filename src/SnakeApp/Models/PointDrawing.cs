using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeApp.Models
{
	public class PointDrawing
	{
		public static async Task DrawPoint(Point p, char character)
		{
			Console.SetCursorPosition(p.X, p.Y);
			Console.Write(character);
		}

		public static async Task ErasePoint(Point p)
		{
			Console.SetCursorPosition(p.X, p.Y);
			Console.Write(" ");
		}
	}
}
