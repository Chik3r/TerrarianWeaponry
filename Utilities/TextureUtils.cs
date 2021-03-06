﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace TerrarianWeaponry.Utilities
{
	public static class TextureUtils
	{
		/// <summary>
		/// Gets the max size for an image. <paramref name="firstOrig"/> and <paramref name="firstMaxSize"/> MUST be the bigger sized values
		/// </summary>
		/// <param name="firstOrig">First origin point</param>
		/// <param name="firstMaxSize">First max size</param>
		/// <param name="secondOrig">Second origin point</param>
		/// <param name="secondMaxSize">Second max size</param>
		/// <param name="clamp">Whether to clamp the result</param>
		/// <returns></returns>
		public static int GetMaxSize(float firstOrig, float firstMaxSize, float secondOrig, float secondMaxSize, bool clamp)
		{
			int returnVal;
			if (firstOrig < firstMaxSize / 2)
				returnVal = (int)((firstMaxSize - firstOrig) + secondOrig); // orig at left
			else
				returnVal = (int)(firstOrig + (secondMaxSize - secondOrig)); // orig at right

			if (clamp)
				return returnVal < firstMaxSize ? (int)firstMaxSize : returnVal;

			return returnVal;
		}

		public static Point GetMaxSize(Point firstOrig, Texture2D firstTexture, Point secondOrig, Texture2D secondTexture)
		{
			return new Point(GetMaxSize(firstOrig.X, firstTexture.Width, secondOrig.X, secondTexture.Width, true),
				GetMaxSize(firstOrig.Y, firstTexture.Height, secondOrig.Y, secondTexture.Height, true));
		}

		public static Texture2D ResizeTexture2D(this Texture2D texture, int newWidth, int newHeight, GraphicsDevice device)
		{
			if (newHeight < texture.Height || newWidth < texture.Width)
				throw new ArgumentException("newHeight or newWidth is smaller than the old texture");

			// Convert the texture to an array of colors
			Color[] colors = new Color[texture.Width * texture.Height];
			Color[] newColors = new Color[newWidth * newHeight];
			texture.GetData(colors);

			// Resize the array and set the new pixels to "new Color()"
			for (int row = 0; row < newHeight; row++)
			{
				for (int column = 0; column < newWidth; column++)
				{
					if (column < texture.Width && row < texture.Height)
						newColors[row * newWidth + column] = colors[row * texture.Width + column];
					else
						newColors[row * newWidth + column] = new Color();
				}
			}

			// Set the data of the texture
			texture = new Texture2D(device, newWidth, newHeight);
			texture.SetData(newColors);
			return texture;
		}

		public static Texture2D MixTexture2D(Texture2D originalTexture, Point originalPoint, Texture2D mergeTexture, Point mergePoint)
		{
			CheckOriginInside(originalTexture, originalPoint);
			CheckOriginInside(mergeTexture, mergePoint);

			Point maxSize = GetMaxSize(originalPoint, originalTexture, mergePoint, mergeTexture);
			int xSize = maxSize.X;
			int ySize = maxSize.Y;

			originalTexture = originalTexture.ResizeTexture2D(xSize, ySize, Main.instance.GraphicsDevice);
			Color[,] colorOriginal = originalTexture.ToColor().To2DColor(originalTexture.Width, originalTexture.Height);
			Color[,] colorMerge = mergeTexture.ToColor().To2DColor(mergeTexture.Width, mergeTexture.Height);

			int startingX = originalPoint.X - mergePoint.X;
			int startingY = originalPoint.Y - mergePoint.Y;

			for (int y = startingY; y < startingY + mergeTexture.Height; y++)
			{
				int currY = y - startingY;
				for (int x = startingX; x < startingX + mergeTexture.Width; x++)
				{
					int currX = x - startingX;

					Color pixel = colorMerge[currY, currX];
					if (pixel.A != 0)
					{
						colorOriginal[y, x] = pixel;
					}
				}
			}

			originalTexture.SetData(colorOriginal.To1DColor(originalTexture.Width, originalTexture.Height));
			return originalTexture;
		}

		private static void CheckOriginInside(Texture2D texture, Point point)
		{
			if (point.X < 0 || point.X >= texture.Width ||
			    point.Y < 0 || point.Y >= texture.Height)
				throw new ArgumentException("The original point is out of bounds\n" +
				                            $"Point: {point}\n" +
				                            $"Texture: width: {texture.Width} - height: {texture.Height}");
		}
	}
}
