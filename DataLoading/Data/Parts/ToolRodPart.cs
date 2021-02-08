using System;
using System.Collections.Generic;

namespace TerrarianWeaponry.DataLoading.Data.Parts
{
	public class ToolRodPart : BasePart
	{
		public override string PartName => "Tool Rod";

		internal override List<Type> ValidMaterials => new List<Type>
		{
			typeof(TestModifierMaterial),
			typeof(TestBothMaterial)
		};
	}
}
