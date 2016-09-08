using SnakeApp.Configuration;
using SnakeApp.Models;

namespace SnakeApp.Factories
{
	public class GameFactory
	{
		private readonly AppSettings _appSettings;

		public GameFactory(AppSettings appSettings)
		{
			_appSettings = appSettings;
		}

		public Game CreateNewGame(World world)
		{
			return new Game(world, new Score(), _appSettings.StartingGameSpeed);
		}
	}
}
