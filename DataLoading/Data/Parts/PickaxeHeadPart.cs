using System.Collections.Generic;

namespace TerrarianWeaponry.DataLoading.Data.Parts
{
	class PickaxeHeadPart : BasePart
	{
		public override string PartName => "Pickaxe Head";

		internal override List<BaseMaterial> ValidMaterials => new List<BaseMaterial>
		{
			new TestMainMaterial(),
			new TestBothMaterial(),
		};
	}
}
