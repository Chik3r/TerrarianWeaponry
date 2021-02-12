using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TerrarianWeaponry.DataLoading.Data
{
	public class TestBothMaterial : BaseMaterial
	{
		public override string MaterialName => "Both";

		public override string Texture => "DataLoading/Data/Materials/Textures/TestBothMaterial";

		public override Point OriginPoint => new Point(20, 10);

		public override int[] MaterialTypes => new int[]
		{
			ItemID.StoneBlock,
			ItemID.Wood,
		};

		public override float? Modifier => 1.6f;

		public override void ModifyStats(ref ItemInfo item, float modifier)
		{
			item.damage = (int) (15 * modifier);
			item.useAnimation = (int) (20 / modifier);
			item.useTime = (int) (20 / modifier);
			item.pick = (int)(40 * modifier);
		}
	}
}
