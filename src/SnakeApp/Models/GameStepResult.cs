namespace SnakeApp.Models
{
	/// <summary>
	/// Represents the results that happened in the world in one iteration.
	/// </summary>
	public class GameStepResult
	{
		public byte ConsumedFoodScore { get; set; }

		public bool GameOver { get; set; }
	}
}
