using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TerrarianWeaponry.DataLoading.Data.Parts
{
	class PickaxeHeadPart : BasePart
	{
		public override string PartName => "Pickaxe Head";

		public override string Description =>
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam ligula nisl, convallis id orci a, " +
			"sollicitudin congue eros. Sed sit amet turpis et erat pretium scelerisque. Curabitur ac gravida nibh";

		internal override List<(BaseMaterial material, TextureInfo textureInfo)> ValidMaterials => new List<(BaseMaterial material, TextureInfo textureInfo)>
		{
			(new TestMainMaterial(), new TextureInfo("DataLoading/Data/Materials/Textures/TestMainMaterial", new Point(9, 10))),
			(new TestBothMaterial(), new TextureInfo("DataLoading/Data/Materials/Textures/TestBothMaterial", new Point(20, 10))),
		};

		public override int MaterialCost => 10;
	}
}
