using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TerrarianWeaponry.DataLoading.Data
{
	public class TestMainMaterial : BaseMaterial
	{
		public override string MaterialName => "Main";

		public override string Texture => "DataLoading/Data/Materials/Textures/TestMainMaterial";

		public override Point OriginPoint => new Point(9, 10);

		public override int[] MaterialTypes => new int[]
		{
			ItemID.IronBar,
		};

		public override void ModifyStats(ref ItemInfo item, float modifier)
		{
			item.damage = (int) (20 * modifier);
			item.scale = modifier;
			item.useAnimation = (int)(15 / modifier);
			item.useTime = (int)(15 / modifier);
			item.pick = (int)(30 * modifier);
		}
	}
}
