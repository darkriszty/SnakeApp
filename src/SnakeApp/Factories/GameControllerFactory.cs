using SnakeApp.Configuration;
using SnakeApp.Controllers;

namespace SnakeApp.Factories
{
	public class GameControllerFactory
	{
		public GameController CreateController()
		{
			var appSettings = new AppSettings();

			var snake = new SnakeFactory(appSettings).CreateSnake();
			var world = new WorldFactory(appSettings).CreateWorld(snake);

			var snakeController = new SnakeController();
			var foodController = new FoodController(snake, snakeController, appSettings);
			var worldController = new WorldController(world, snakeController, foodController);
			var gameController = new GameController(new GameFactory(appSettings), world, worldController);

			return gameController;
		}
	}
}
