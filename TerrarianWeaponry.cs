using Terraria.ModLoader;
using TerrarianWeaponry.DataLoading;

namespace TerrarianWeaponry
{
	public class TerrarianWeaponry : Mod
	{
		public static TerrarianWeaponry Instance => ModContent.GetInstance<TerrarianWeaponry>();

		public override void Load()
		{
			DataLoader.LoadData();
		}
	}
}