using System;
using System.Threading.Tasks;

namespace SnakeApp.Models
{
	public class Food
	{
		private bool _drawnAsRotten;
		private readonly byte _score;
		private readonly Point _position;
		private readonly DateTime _dateCreated;
		private readonly byte _secondsToLive;

		public Food(byte score, Point position, byte secondsToLive)
		{
			_score = score;
			_position = position;
			_secondsToLive = secondsToLive;
			_dateCreated = DateTime.Now;
		}

		public Point Position
		{
			get { return _position; }
		}

		private bool _IsExpired
		{
			get { return (DateTime.Now - _dateCreated).TotalSeconds > _secondsToLive; }
		}

		public bool IsRotten
		{
			get { return _IsExpired &&_drawnAsRotten; }
		}

		public async Task Draw()
		{
			// after the food becomes old the world must first draw it (aka remove it)
			// then the CanRemove property returns true such that it is removed from the world an the position is freed up
			if (_IsExpired)
			{
				await PointDrawing.ErasePoint(_position);
				_drawnAsRotten = true;
			}
			else
			{
				await PointDrawing.DrawPoint(_position, '*');
			}
		}
	}
}