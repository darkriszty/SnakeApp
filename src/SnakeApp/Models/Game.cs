namespace SnakeApp.Models
{
	/// <summary>
	/// It's an abstract game object, having a "real" world.
	/// </summary>
	public class Game
	{
		public Game(World world, Score score, int speed)
		{
			World = world;
			Score = score;
			Speed = speed;
		}

		public World World { get; set; }

		public Score Score { get; set; }

		public int Speed { get; set; }

		public bool IsPaused { get; set; }

		public bool IsOver { get; set; }
	}
}
