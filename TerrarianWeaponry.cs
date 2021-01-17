using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using TerrarianWeaponry.Items;
using TerrarianWeaponry.JsonData;

namespace TerrarianWeaponry
{
	public class TerrarianWeaponry : Mod
	{
		public override void Load()
		{
			//System.IO.Directory.GetFiles(,,)
			//	;
			//GetFileBytes

			var fileFieldInfo = typeof(TerrarianWeaponry).GetProperty("File", BindingFlags.Instance | BindingFlags.NonPublic);
			TmodFile file = fileFieldInfo.GetValue(this) as TmodFile;

			var filesField = typeof(TmodFile).GetField("files", BindingFlags.Instance | BindingFlags.NonPublic);
			IDictionary<string, TmodFile.FileEntry> files = filesField.GetValue(file) as IDictionary<string, TmodFile.FileEntry>;
			//Logger.Debug(string.Join("\n", files.Select(x => $"{x.Key} - {x.Value.Name}")));

			byte[] jsonBytes = GetFileBytes("JsonData/data.json");
			var a = /*MainData.FromJson(*/Encoding.UTF8.GetString(jsonBytes)/*)*/;
			//var b = JsonConvert.DeserializeObject<string>(a);
			//var c = MainData.FromJson(a);
			Logger.Debug(a);
			var c = JsonConvert.DeserializeObject<MainData>(a);

			//Texture2D stickTexture = GetTexture("Materials/Stick");
			//Point stickOrig = new Point(21, 10);
			//Texture2D headTexture = GetTexture("Materials/Head");
			//Point headOrig = new Point(9, 10);
			//Texture2D anotherHeadTexture = GetTexture("Materials/AnotherHead");
			//Point anohterHeadOrig = new Point(9, 10);

			//Texture2D a = Utilities.MixTexture2D(stickTexture, stickOrig, headTexture, headOrig);
			//Texture2D b = Utilities.MixTexture2D(stickTexture, stickOrig, anotherHeadTexture, anohterHeadOrig);

			//  10.000 items =  3 seconds
			// 100.000 items = 30 seconds - 150 MB of RAM
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
			//AddItem("SuperPick", new BasePickaxe(new ItemInfo
			//{

			//	damage = 200,
			//	width = 32,
			//	height = 32,
			//	pick = 100,
			//	useTime = 20,
			//	useAnimation = 20,
			//	useStyle = ItemUseStyleID.SwingThrow
			//}, a));

			//AddItem("SuperColorPick", new BasePickaxe(new ItemInfo
			//{

			//	damage = 170,
			//	width = 32,
			//	height = 32,
			//	pick = 110,
			//	useTime = 20,
			//	useAnimation = 20,
			//	useStyle = ItemUseStyleID.SwingThrow
			//}, b));
		}
	}
}