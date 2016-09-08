using System;

namespace SnakeApp.Configuration
{
	public class AppSettings
	{
		private static byte MAX_WIDTH = 80;
		private static byte MAX_HEIGHT = 25;

		public byte WorldWidth { get; } = Math.Min((byte)(Console.WindowWidth - 1), MAX_WIDTH);

		public byte WorldHeight { get; } = Math.Min((byte)(Console.WindowHeight - 1), MAX_HEIGHT);

		public byte InitialSnakeSize { get; } = 5;

		public int StartingGameSpeed { get; } = 100;

		public int MaxFoodCount { get; } = 1;

		public byte FoodScore { get; } = 1;

		public byte FoodTTLInSeconds { get; } = 10;
	}
}
