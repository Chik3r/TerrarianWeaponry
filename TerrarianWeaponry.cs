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
			//var texture = Main.itemTexture[item.type];
			Texture2D stickTexture = GetTexture("Materials/Stick");
			Point stickOrig = new Point(21, 10);
			Texture2D headTexture = GetTexture("Materials/Head");
			Point headOrig = new Point(9, 10);
			Texture2D anotherHeadTexture = GetTexture("Materials/AnotherHead");
			Point anohterHeadOrig = new Point(9, 10);

			Texture2D a = Utilities.MixTexture2D(stickTexture, stickOrig, headTexture, headOrig);
			Texture2D b = Utilities.MixTexture2D(stickTexture, stickOrig, anotherHeadTexture, anohterHeadOrig);

			//  10.000 items =  3 seconds
			// 100.000 items = 30 seconds
			//for (int i = 0; i < 10000; i++)
			//{
			//	var _ = Utilities.MixTexture2D(stickTexture, stickOrig, headTexture, headOrig);

			//	AddItem("Test" + i, new BasePickaxe(new ItemInfo
			//	{

			//		damage = i,
			//		width = 32,
			//		height = 32,
			//		pick = 100,
			//		useTime = 20,
			//		useAnimation = 20,
			//		useStyle = ItemUseStyleID.SwingThrow
			//	}, a));
			//}
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
		}
	}
}