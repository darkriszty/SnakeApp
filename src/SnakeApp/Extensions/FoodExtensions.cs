using SnakeApp.Graphics;
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
	}
}
