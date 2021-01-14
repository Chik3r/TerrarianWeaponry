using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrarianWeaponry.Items;

namespace TerrarianWeaponry
{
	public class TerrarianWeaponry : Mod
	{
		public override void Load()
		{
			//base.Load();


			//var texture = Main.itemTexture[item.type];
			Texture2D stickTexture = GetTexture("Materials/Stick");
			Point stickOrig = new Point(21, 10);
			Texture2D headTexture = GetTexture("Materials/Head");
			Point headOrig = new Point(9, 10);
			Texture2D anotherHeadTexture = GetTexture("Materials/AnotherHead");
			Point anohterHeadOrig = new Point(9, 10);

			Texture2D a = Utilities.MixTexture2D(stickTexture, stickOrig, headTexture, headOrig);
			Texture2D b = Utilities.MixTexture2D(stickTexture, stickOrig, anotherHeadTexture, anohterHeadOrig);

			//Point maxSize = Utilities.GetMaxSize(stickOrig, stickTexture, headOrig, headTexture);
			//int xSize = (int) maxSize.X;
			//int ySize = (int) maxSize.Y;

			////var finalTexture = new Texture2D(Main.instance.GraphicsDevice, xSize, ySize);
			//stickTexture = stickTexture.ResizeTexture2D(xSize, ySize, Main.instance.GraphicsDevice);
			//Color[,] colorStick = stickTexture.ToColor().To2DColor(stickTexture.Width, stickTexture.Height);
			//Color[,] colorHead = headTexture.ToColor().To2DColor(headTexture.Width, headTexture.Height);

			//int unsafeMaxX = Utilities.GetMaxSize(stickOrig.X, stickTexture.Width, headOrig.X, headTexture.Width, true);
			//int unsafeMaxY = Utilities.GetMaxSize(stickOrig.Y, stickTexture.Height, headOrig.Y, headTexture.Height, true);

			//int startingX = stickOrig.X - headOrig.X;
			//int startingY = stickOrig.Y - headOrig.Y;

			//for (int y = startingY; y < startingY + headTexture.Height; y++)
			//{
			//	int currY = y - startingY;
			//	for (int x = startingX; x < startingX + headTexture.Width; x++)
			//	{
			//		int currX = x - startingX;

			//		Color pixel = colorHead[currY, currX];
			//		if (pixel.A != 0)
			//		{
			//			colorStick[y, x] = pixel;
			//		}
			//	}
			//}

			//stickTexture.SetData(colorStick.To1DColor(stickTexture.Width, stickTexture.Height));

			AddItem("SuperPick", new BasePickaxe(new ItemInfo
			{

				damage = 200,
				width = 32,
				height = 32,
				pick = 100,
				useTime = 20,
				useAnimation = 20,
				useStyle = ItemUseStyleID.SwingThrow
			}, a));

			AddItem("SuperColorPick", new BasePickaxe(new ItemInfo
			{

				damage = 170,
				width = 32,
				height = 32,
				pick = 110,
				useTime = 20,
				useAnimation = 20,
				useStyle = ItemUseStyleID.SwingThrow
			}, b));

			//using (MemoryStream stream = new MemoryStream(GetFileBytes(Path.Combine("Materials", "Head.png"))))
			//headTexture = Texture2D.FromStream(Main.instance.GraphicsDevice, stream);

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
		}
	}
}