using Microsoft.Xna.Framework;

namespace TerrarianWeaponry.DataLoading
{
	struct TextureInfo
	{
		public string Texture;
		public Point OriginPoint;

		public TextureInfo(string texture, Point originPoint)
		{
			Texture = texture;
			OriginPoint = originPoint;
		}
	}
}
