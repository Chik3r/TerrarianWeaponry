using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TerrarianWeaponry.DataLoading.Data.Parts
{
	class PickaxeHeadPart : BasePart
	{
		public override string PartName => "Pickaxe Head";

		internal override List<(BaseMaterial material, TextureInfo textureInfo)> ValidMaterials => new List<(BaseMaterial material, TextureInfo textureInfo)>
		{
			(new TestMainMaterial(), new TextureInfo("DataLoading/Data/Materials/Textures/TestMainMaterial", new Point(9, 10))),
			(new TestBothMaterial(), new TextureInfo("DataLoading/Data/Materials/Textures/TestBothMaterial", new Point(20, 10))),
		};
	}
}
