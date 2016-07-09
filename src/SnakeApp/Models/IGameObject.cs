using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeApp.Models
{
	interface IGameObject
	{
		Task Advance();
		Task Draw();
	}
}
