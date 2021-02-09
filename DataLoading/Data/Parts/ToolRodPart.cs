using System.Collections.Generic;

namespace TerrarianWeaponry.DataLoading.Data.Parts
{
	public class ToolRodPart : BasePart
	{
		public override string PartName => "Tool Rod";

		internal override List<BaseMaterial> ValidMaterials => new List<BaseMaterial>
		{
			new TestModifierMaterial(),
			new TestBothMaterial(),
		};
	}
}
