using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace TerrarianWeaponry.DataLoading
{
	public abstract class TextureItem : ModItem
	{
		public override string Texture => "TerrarianWeaponry/MissingItem";

		public override bool CloneNewInstances => true;

		protected readonly Texture2D _texture;

		public TextureItem(Texture2D texture) => _texture = texture;

		public TextureItem() { }

		public override void SetStaticDefaults()
		{
			if (_texture != null)
				Main.itemTexture[item.type] = _texture;
		}

		public override bool Autoload(ref string name) => false;
	}
}
