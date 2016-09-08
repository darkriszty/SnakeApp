using SnakeApp.Configuration;
using SnakeApp.Models;

namespace SnakeApp.Factories
{
	public class WorldFactory
	{
		private readonly AppSettings _appSettings;

		public WorldFactory(AppSettings appSettings)
		{
			_appSettings = appSettings;
		}

		public World CreateWorld(Snake snake)
		{
			return new World(snake, _appSettings.WorldWidth, _appSettings.WorldHeight);
		}
	}
}
