using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TerrarianWeaponry.Utilities
{
	public static class ColorUtils
	{
		public static Color[] ToColor(this Texture2D texture)
		{
			Color[] colors = new Color[texture.Width * texture.Height];
			texture.GetData(colors);
			return colors;
		}

		/// <summary>
		/// Converts a 1D color array to a 2D color array. The first dimension gets the rows, and the second dimension gets the columns
		/// </summary>
		/// <param name="colors">The array to convert</param>
		/// <param name="width">The width of the array</param>
		/// <param name="height">The height of the array</param>
		/// <returns></returns>
		public static Color[,] To2DColor(this Color[] colors, int width, int height)
		{
			Color[,] grid = new Color[height, width];
			for (int row = 0; row < height; row++)
			{
				for (int column = 0; column < width; column++)
				{
					grid[row, column] = colors[row * width + column];
				}
			}

			return grid;
		}

		/// <summary>
		/// Converts a 2D color array to a 1D color array.
		/// </summary>
		/// <param name="colors">The array to convert</param>
		/// <param name="width">The width of the array</param>
		/// <param name="height">The height of the array</param>
		/// <returns></returns>
		public static Color[] To1DColor(this Color[,] colors, int width, int height)
		{
			Color[] grid = new Color[width * height];
			int write = 0;
			for (int row = 0; row < height; row++)
			{
				for (int column = 0; column < width; column++)
				{
					grid[write++] = colors[row, column];
				}
			}

			return grid;
		}
	}
}
