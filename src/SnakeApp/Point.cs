﻿namespace SnakeApp
{
	public class Point
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Point() { }
		public Point(Point other) : this()
		{
			X = other.X;
			Y = other.Y;
		}
	}
}