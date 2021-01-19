using Terraria.ID;

namespace TerrarianWeaponry.Items
{
	public class BasePickaxe : BaseItem
	{
		public override string Texture => "TerrarianWeaponry/Items/BasePickaxe";

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
			return true;
		}
	}
}
