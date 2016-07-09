using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var gameController = new GameController();
			Task.Run(async () => await gameController.StartNewGame()).Wait();
		}
	}
}
