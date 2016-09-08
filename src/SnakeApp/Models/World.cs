namespace SnakeApp.Models
{
	public class World
	{
		public World(Snake snake, byte width, byte height)
		{
			Snake = snake;
			Width = width;
			Height = height;
		}

		public bool BordersDrawn { get; set; }

		public Snake Snake { get; set; }

		public byte Width { get; set; }

		public byte Height { get; set; }
	}
}
