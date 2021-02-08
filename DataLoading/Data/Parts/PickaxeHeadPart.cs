using System;
using System.Collections.Generic;

namespace TerrarianWeaponry.DataLoading.Data.Parts
{
	class PickaxeHeadPart : BasePart
	{
		public override string PartName => "Pickaxe Head";

		internal override List<Type> ValidMaterials => new List<Type>
		{
			typeof(TestMainMaterial),
			typeof(TestBothMaterial)
		};
	}
}
