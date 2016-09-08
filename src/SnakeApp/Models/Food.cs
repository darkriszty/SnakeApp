using SnakeApp.Drawing;
using System;

namespace SnakeApp.Models
{
	public class Food
	{
		public Food()
		{
			DateCreated = DateTime.Now;
		}

		public Food(byte score, Point position, byte secondsToLive) : this()
		{
			Score = score;
			Position = position;
			SecondsToLive = secondsToLive;
		}

		public Point Position { get; private set; }

		public DateTime DateCreated { get; private set; }

		public byte Score { get; private set; }

		public bool DrawnAsRotten { get; set; }

		public bool IsConsumed { get; set; }

		public byte SecondsToLive { get; set; }
	}
}