using System.Text;
using Terraria.ModLoader;
using TerrarianWeaponry.JsonData;

namespace TerrarianWeaponry
{
	public class TerrarianWeaponry : Mod
	{
		internal DataLoader loader;

		public override void Load()
		{
			byte[] jsonBytes = GetFileBytes("JsonData/data.json");
			string jsonString = Encoding.UTF8.GetString(jsonBytes);
			var mainData = MainData.FromJson(jsonString);

			loader = new DataLoader(mainData);
			loader.LoadData();
		}
	}
}