using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TerrarianWeaponry.DataLoading.Data
{
	public class TestModifierMaterial : BaseMaterial
	{
		public override string MaterialName => "Modifier";

		public override string Texture => "DataLoading/Data/Materials/Textures/TestModifierMaterial";

		public override Point OriginPoint => new Point(21, 10);

		public override int[] MaterialTypes => new int[]
		{
			ItemID.GoldBar,
		};

		public override float? Modifier => 1.5f;
	}
}
