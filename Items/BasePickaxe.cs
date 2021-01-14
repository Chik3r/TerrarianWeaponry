using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace TerrarianWeaponry.Items
{
	public class BasePickaxe : BaseItem
	{
		public override string Texture => "TerrarianWeaponry/Items/BasePickaxe";
		private float _redColor;
		private float _greenColor;
		private float _blueColor;
		private readonly Texture2D _texture;

		public BasePickaxe() { }

		public BasePickaxe(ItemInfo info/*, float red = 1, float green = 1, float blue = 1*/, Texture2D texture = null)
		{
			_info = info;
			//_redColor = 1;
			//_greenColor = green;
			//_blueColor = blue;
			_texture = texture;
		}

		public override void SetStaticDefaults()
		{
			if (_texture != null)
			{
				Main.itemTexture[item.type] = _texture;
				_info.width = _texture.Width;
				_info.height = _texture.Height;
			}
		}

		public override void SetDefaults()
		{
			//var texture = Main.itemTexture[item.type];
			//if (texture != null)
			//{
			//	var colorArr = new Color[texture.Width * texture.Height];
			//	texture.GetData(colorArr);
			//	var colors = colorArr.To2DColor(texture.Width, texture.Height);

			//	var a = from Color col in colors
			//		select col.MultiplyColor(_redColor, _greenColor, _blueColor);
			//	colors = a.ToArray().To2DColor(texture.Width, texture.Height);

			//	//colors[8, 22] = Color.Red;
			//	//colors[8, 23] = Color.Red;
			//	//colors[9, 22] = Color.Red;
			//	//colors[9, 23] = Color.Red;
			//	texture.SetData(colors.To1DColor(texture.Width, texture.Height));
			//	Main.itemTexture[item.type] = texture;
			//}

			SetDefaultsFromInfo();
		}

		public override bool Autoload(ref string name)
		{
			_info = new ItemInfo
			{
				damage = 20,
				width = 32,
				height = 32,
				pick = 100,
				useTime = 20,
				useAnimation = 20,
				useStyle = ItemUseStyleID.SwingThrow
			};
			//_color = new Color(183, 88, 25);
			_redColor = 3.6f;
			_greenColor = 1.76f;
			_blueColor = .5f;
			return true;
		}

		public override void UseStyle(Player player)
		{
			Main.NewText("use" + item.type);
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale,
			int whoAmI)
		{
			//Texture2D texture = Main.itemTexture[item.type];
			//Vector2 pos = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * .5f + 2f);

			//spriteBatch.Draw(texture, pos, null, new Color(183, 88, 25), rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);

			//return false;
			return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor,
			Vector2 origin, float scale)
		{
			//Texture2D texture = Main.itemTexture[item.type];
			////Vector2 pos = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * .5f + 2f);

			//spriteBatch.Draw(texture, position, null, new Color(183, 88, 25, 255), 0, origin, scale, SpriteEffects.None, 0f);

			//return false;
			return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}
	}
}
