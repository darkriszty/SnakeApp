using System.Threading.Tasks;

namespace SnakeApp.Models
{
    public class Food
    {
        private readonly byte _score;
        private readonly Point _position;

        public Food(byte score, Point position)
        {
            _score = score;
            _position = position;
        }

        public async Task Draw()
		{
            await PointDrawing.DrawPoint(_position, '*');
        }
    }
}