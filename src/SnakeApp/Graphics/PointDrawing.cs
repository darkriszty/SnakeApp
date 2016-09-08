using System;

namespace SnakeApp.Graphics
{
	public class PointDrawing
	{
		public static void DrawPoint(Point p, char character)
		{
			Console.SetCursorPosition(p.X, p.Y);
			Console.Write(character);
		}

		public static void ErasePoint(Point p)
		{
			Console.SetCursorPosition(p.X, p.Y);
			Console.Write(" ");
		}
	}
}
