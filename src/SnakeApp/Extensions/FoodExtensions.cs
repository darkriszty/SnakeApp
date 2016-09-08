using SnakeApp.Models;
using System;

namespace SnakeApp.Extensions
{
	public static class FoodExtensions
	{
		public static bool IsExpired(this Food food)
		{
			return (DateTime.Now - food.DateCreated).TotalSeconds > food.SecondsToLive;
		}

		/// <summary>
		/// Can be removed from the world either after it was expired and drawn as expirted, or if it got consumed
		/// </summary>
		public static bool CanRemove(this Food food)
		{
			return (food.IsExpired() && food.DrawnAsRotten) || food.IsConsumed;
		}

		public static void Draw(this Food food)
		{
			// after the food becomes old the world must first draw it (aka remove it)
			// then the CanRemove property returns true such that it is removed from the world and the position is freed up
			if (food.IsExpired())
			{
				PointDrawing.ErasePoint(food.Position);
				food.DrawnAsRotten = true;
			}
			else
			{
				PointDrawing.DrawPoint(food.Position, '*');
			}
		}
	}
}
