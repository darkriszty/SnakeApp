using SnakeApp.Factories;

namespace SnakeApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var gameController = new GameControllerFactory().CreateController();

			gameController.StartNewGame();
		}
	}
}
