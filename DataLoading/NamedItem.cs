using Microsoft.Xna.Framework.Graphics;

namespace TerrarianWeaponry.DataLoading
{
	public class NamedItem : TextureItem
	{
		private readonly string _displayName;

		public NamedItem(Texture2D texture, string displayName) : base(texture) => _displayName = displayName;

		public NamedItem() { }

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault(_displayName);
		}
	}
}
